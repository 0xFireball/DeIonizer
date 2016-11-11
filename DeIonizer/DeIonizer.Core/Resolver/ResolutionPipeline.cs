using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeIonizer.Core.Resolver
{
    public class ResolutionPipeline
    {
        public IList<IResolver> Resolvers { get; } = new List<IResolver> { new IPAddressResolver(), new UriResolver(), new HostnameResolver() };

        internal Task<string> Resolve(string target)
        {
            return Task.Run(() =>
            {
                try
                {
                    var result = Resolvers.FirstOrDefault(r => r.CanResolve(target)).Resolve(target);
                    return result;
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}