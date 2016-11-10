using DeIonizer.Core.Resolver;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeIonizer.States
{
    public class MainWindowState
    {
        public async Task VisitIridiumIon()
        {
            Process.Start("https://iridiumion.xyz");
        }

        public async Task<string> LockTarget(string targetLocation)
        {
            //Asynchronously resolve location
            var result = await new ResolutionPipeline().Resolve(targetLocation);
            if (string.IsNullOrWhiteSpace(result))
            {
                result = null;
            }
            return result;
        }
    }
}