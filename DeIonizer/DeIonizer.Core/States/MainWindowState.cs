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

        public async Task LockTarget(string targetLocation)
        {
            //Asynchronously resolve location
            await new ResolutionPipeline().Resolve(targetLocation);
        }
    }
}