using System.Net;

namespace DeIonizer.Core.Resolver
{
    public class IPAddressResolver : IResolver
    {
        public bool CanResolve(string target)
        {
            IPAddress address;
            return IPAddress.TryParse(target, out address);
        }

        public string Resolve(string target)
        {
            IPAddress address;
            IPAddress.TryParse(target, out address);
            return address.ToString();
        }
    }
}