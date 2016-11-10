using System.Diagnostics;
using System.Threading.Tasks;

namespace DeIonizer.States
{
    internal class MainWindowState
    {
        public async Task VisitIridiumIon()
        {
            Process.Start("https://iridiumion.xyz");
        }
    }
}