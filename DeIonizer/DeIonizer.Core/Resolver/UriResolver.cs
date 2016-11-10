using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace DeIonizer.Core.Resolver
{
    public class UriResolver : IResolver
    {
        public bool CanResolve(string target)
        {
            Uri result;
            return Uri.TryCreate(target, UriKind.RelativeOrAbsolute, out result);
        }

        public string Resolve(string target)
        {
            Uri resultUri;
            Uri.TryCreate(target, UriKind.RelativeOrAbsolute, out resultUri);
            var dnsName = resultUri.IsAbsoluteUri ? resultUri.DnsSafeHost : resultUri.ToString();
            return Dns.GetHostEntry(dnsName).AddressList
                .FirstOrDefault(x => (x.AddressFamily == AddressFamily.InterNetwork) || (x.AddressFamily == AddressFamily.InterNetworkV6))
                .ToString();
        }
    }
}