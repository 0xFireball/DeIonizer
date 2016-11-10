using DeIonizer.States;
using ReactiveUI;
using System.Reactive;

namespace DeIonizer.VM
{
    internal class MainWindowVM
    {
        private readonly MainWindowState state = new MainWindowState();
        public ReactiveCommand<Unit> VisitIridiumIonCommand { get; }
        public string ApplicationVersion { get; }

        public MainWindowVM()
        {
            VisitIridiumIonCommand = ReactiveCommand.CreateAsyncTask(_ => state.VisitIridiumIon());
        }
    }
}