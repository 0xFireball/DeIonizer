using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace DeIonizer.Core.Resolver
{
    public class HostnameResolver : IResolver
    {
        public bool CanResolve(string target)
        {
            //make sure it's a recognized hostname
            return Uri.CheckHostName(target) != UriHostNameType.Unknown;
        }

        public string Resolve(string target)
        {
            return Dns.GetHostEntry(target).AddressList
                .FirstOrDefault(x => (x.AddressFamily == AddressFamily.InterNetwork) || (x.AddressFamily == AddressFamily.InterNetworkV6))
                .ToString();
        }
    }
}