/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ToolGood.WebCommon
{
    public class IpFirewallMiddleware
    {
        private readonly RequestDelegate _next;

        public IpFirewallMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsLocalhost(context) == false) {
                var ipFirewall = IpFirewallHelper.GetIpFirewall();
                if (ipFirewall.IsAllowed(GetIPAddress(context)) == false) {
                    context.Response.StatusCode = 404;
                    return;
                }
            }
            await _next.Invoke(context);
        }

        private IPAddress GetIPAddress(HttpContext context)
        {
            return IPAddress.Parse(context.GetRealIP());
            //return context.Conn.RemoteIpAddress;
        }

        public bool IsLocalhost(HttpContext context)
        {
            var localIpAddress = context.Connection.LocalIpAddress;
            var remoteIpAddress = context.Connection.RemoteIpAddress;

            var isLocalhost =
                (remoteIpAddress != null
                    && remoteIpAddress.ToString() != "::1"
                    && remoteIpAddress.Equals(localIpAddress))
                || IPAddress.IsLoopback(remoteIpAddress);
            return isLocalhost;
        }



    }
}
