/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ToolGood.WebCommon
{
    public class IpFirewallHelper
    {
        private static IpFirewallHelper _ipFirewallHelper;
        #region class info
        /// <summary>
        /// Type to represent a CIDR notation (e.g. "120.30.24.123/20").
        /// </summary>
        class CIDRNotation
        {
            /// <summary>
            /// The IP address part of the CIDR notation.
            /// </summary>
            public readonly IPAddress Address;

            /// <summary>
            /// The mask bits of the CIDR notation.
            /// </summary>
            public readonly int MaskBits;

            public CIDRNotation(IPAddress address,int maskBits)
            {
                Address = address;
                MaskBits= maskBits;
            }

            public static bool TryParse(string ip, out CIDRNotation notation)
            {
                if (string.IsNullOrEmpty(ip)) { notation = null; return false; }

                var parts = ip.Split('/');
                if (parts.Length != 2) { notation = null; return false; }

                var isValid = IPAddress.TryParse(parts[0], out var address);
                if (!isValid) { notation = null; return false; }

                var maskBits = Convert.ToInt32(parts[1], 10);
                var maxMaskBit = address.AddressFamily == AddressFamily.InterNetwork ? 32 : 128;
                if (maskBits < 0 || maskBits > maxMaskBit) { notation = null; return false; }

                notation = new CIDRNotation(address, maskBits);
                return true;
            }

            /// <summary>
            /// Checks if an <paramref name="address"/> is within the address space defined by this CIDR notation.
            /// </summary>
            public bool Contains(IPAddress address) =>
                CompareAddressBytes(
                    this.Address.GetAddressBytes(),
                    GetMappedAddressBytes(address),
                    this.MaskBits);

            private byte[] GetMappedAddressBytes(IPAddress address)
            {
                return address.IsIPv4MappedToIPv6 ? address.MapToIPv4().GetAddressBytes() : address.GetAddressBytes();
            }

            private static bool CompareAddressBytes(byte[] cidr, byte[] address, int bits)
            {
                if (cidr.Length != address.Length) return false;

                var index = 0;

                for (; bits >= 8; bits -= 8) {
                    if (address[index] != cidr[index])
                        return false;
                    index++;
                }

                if (bits > 0) {
                    var mask = (byte)~(255 >> bits);
                    if ((address[index] & mask) != (cidr[index] & mask))
                        return false;
                }

                return true;
            }

        }
        #endregion
        private List<IPAddress> _blackList = new List<IPAddress>();
        private List<CIDRNotation> _blackNotation = new List<CIDRNotation>();
        private List<IPAddress> _whiteList = new List<IPAddress>();
        private List<CIDRNotation> _whiteNotation = new List<CIDRNotation>();
        /// <summary>
        /// 0)禁用，1)黑名单过滤，2）白名单过滤
        /// </summary>
        private int useIpType = 0;

        public bool IsAllowed(string ip)
        {
            var ipAddress = IPAddress.Parse(ip);
            return IsAllowed(ipAddress);
        }
        public bool IsAllowed(IPAddress ipAddress)
        {
            if (useIpType == 1) {
                if (MatchesAnyIPAddress(ipAddress, _blackList)) { return false; }
                if (MatchesAnyIPAddressRange(ipAddress, _blackNotation)) { return false; }
            } else if (useIpType == 2) {
                if (MatchesAnyIPAddress(ipAddress, _whiteList)) { return true; }
                if (MatchesAnyIPAddressRange(ipAddress, _whiteNotation)) { return true; }
                return false;
            }
            return true;
        }

        public void CloseFirewall()
        {
            useIpType = 0;
        }
        public void UseIpBanList()
        {
            useIpType = 1;
        }
        public void UseIpAllowList()
        {
            useIpType = 2;
        }

        public void SetIpBanList(List<string> ips)
        {
            var ipList = new List<IPAddress>();
            var ipNotation = new List<CIDRNotation>();
            foreach (var ip in ips) {
                if (ip.Contains("/")) {
                    if (CIDRNotation.TryParse(ip,out CIDRNotation notation)) {
                        ipNotation.Add(notation);
                    }
                } else {
                    if (IPAddress.TryParse(ip,out IPAddress address)) {
                        ipList.Add(address);
                    }
                }
            }
            _blackList = ipList;
            _blackNotation = ipNotation;
        }

        public void SetIpAllowList(List<string> ips)
        {
            var ipList = new List<IPAddress>();
            var ipNotation = new List<CIDRNotation>();
            foreach (var ip in ips) {
                if (ip.Contains("/")) {
                    if (CIDRNotation.TryParse(ip, out CIDRNotation notation)) {
                        ipNotation.Add(notation);
                    }
                } else {
                    if (IPAddress.TryParse(ip, out IPAddress address)) {
                        ipList.Add(address);
                    }
                }
            }
            _whiteList = ipList;
            _whiteNotation = ipNotation;
        }


        public static IpFirewallHelper GetIpFirewall()
        {
            if (_ipFirewallHelper==null) {
                _ipFirewallHelper = new IpFirewallHelper();
            }
            return _ipFirewallHelper;
        }

        private bool MatchesAnyIPAddress(IPAddress remoteIpAddress, List<IPAddress> iPAddresses)
        {
            if (iPAddresses != null && iPAddresses.Count > 0)
                foreach (var ip in iPAddresses)
                    if (IsEqualTo(ip, remoteIpAddress))
                        return true;

            return false;
        }
        private bool MatchesAnyIPAddressRange(IPAddress remoteIpAddress, List<CIDRNotation> _cidrNotations)
        {
            if (_cidrNotations != null && _cidrNotations.Count > 0)
                foreach (var cidr in _cidrNotations)
                    if (cidr.Contains(remoteIpAddress))
                        return true;

            return false;
        }
        private byte[] GetMappedAddressBytes(IPAddress address)
        {
            return address.IsIPv4MappedToIPv6 ? address.MapToIPv4().GetAddressBytes() : address.GetAddressBytes();
        }
        private bool IsEqualTo(IPAddress address, IPAddress otherAddress)
        {
            return IsEqualTo(address.GetAddressBytes(), GetMappedAddressBytes(otherAddress));
        }
        private bool IsEqualTo(byte[] bytes, byte[] otherBytes)
        {
            if (bytes == null && otherBytes == null) return true;
            if (bytes == null) return false;
            if (otherBytes == null) return false;
            if (bytes.Length != otherBytes.Length) return false;

            for (var i = 0; i < bytes.Length; i++)
                if (bytes[i] != otherBytes[i]) return false;

            return true;
        }
    }
}
