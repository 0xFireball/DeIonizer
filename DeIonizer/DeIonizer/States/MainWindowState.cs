using System.Diagnostics;

namespace DeIonizer.States
{
    internal class MainWindowState
    {
        public void VisitIridiumIon()
        {
            Process.Start("https://iridiumion.xyz");
        }
    }
}