using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using Microsoft.Win32;

namespace ToolGood.SqlOnline.Browser.Core
{
    static class Config
    {

        const string FilePath = "setting.ini";
        public static string GetUrl()
        {
            if (File.Exists(FilePath)) {
                IniFile iniFile = new IniFile(FilePath);
                var section = iniFile.Section("Setting");
                var url = section.Get<string>("Url");
                iniFile = null;
                if (string.IsNullOrEmpty(url)) {
                    return "http://toolgood.com";
                }
                return url;
            }

            return "http://toolgood.com";
        }


        public static string GetUserAgent()
        {
            return "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.75 Safari/537.36 SQLonline/2021";
        }


        private static string _mac;
        public static string GetMAC()
        {
            if (_mac == null) {
                try {
                    List<string> macs = new List<string>();
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc) {
                        if ((bool)mo["IPEnabled"]) {
                            var mac = mo["MacAddress"].ToString();
                            macs.Add(mac);
                        }
                    }
                    _mac = string.Join("|", macs); ;
                } catch (Exception e) {
                }
            }
            return _mac;
        }
        private static string _mc;
        public static string GetMachineCode()
        {
            if (_mc == null) {
                string s = "ToolGood.SqlOnline.Browser";
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher();
                managementObjectSearcher.Query = new ObjectQuery("select * from Win32_Processor");
                foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                    s += "|" + managementObject.GetPropertyValue("ProcessorId");

                managementObjectSearcher.Query = new ObjectQuery("select * from Win32_BaseBoard");
                foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                    s += "|" + managementObject.GetPropertyValue("SerialNumber");

                managementObjectSearcher.Query = new ObjectQuery("select * from Win32_BIOS");
                foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                    s += "|" + managementObject.GetPropertyValue("SerialNumber");

                _mc = HashUtil.GetMd5String(s);
            }
            return _mc;

        }

        public static String GetDefaultWebBrowserFilePath()
        {
            string _BrowserKey1 = @"Software\Clients\StartmenuInternet\";
            string _BrowserKey2 = @"shell\open\command";
            string outPath;

            RegistryKey localKey;
            if (Environment.Is64BitOperatingSystem) {
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            } else {
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }

            RegistryKey _RegistryKey = localKey.OpenSubKey(_BrowserKey1, false);
            var names = _RegistryKey.GetSubKeyNames();
            if (names.Contains("Google Chrome")) {
                var key = _RegistryKey.OpenSubKey("Google Chrome").OpenSubKey(_BrowserKey2);
                outPath = key.GetValue("").ToString();
            } else if (names.Any(q => q.StartsWith("Firefox"))) {
                var name = names.Where(q => q.StartsWith("Firefox")).FirstOrDefault();
                var key = _RegistryKey.OpenSubKey(name).OpenSubKey(_BrowserKey2);
                outPath = key.GetValue("").ToString();
            } else {
                String name = _RegistryKey.GetValue("").ToString();
                var key = _RegistryKey.OpenSubKey(name).OpenSubKey(_BrowserKey2);
                outPath = key.GetValue("").ToString();
            }
            _RegistryKey.Close();

            if (outPath.Contains("\"")) {
                outPath = outPath.TrimStart('"');
                outPath = outPath.Substring(0, outPath.IndexOf('"'));
            }
            return outPath;
        }


    }
}
