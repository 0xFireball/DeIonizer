using DeIonizer.States;
using ReactiveUI;
using System.Reactive;

namespace DeIonizer.VM
{
    internal class MainWindowVM : ReactiveObject    
    {
        private readonly MainWindowState state = new MainWindowState();

        public ReactiveCommand<Unit> VisitIridiumIonCommand { get; }

        public string ApplicationVersion => $"v{typeof(MainWindowVM).Assembly.GetName().Version.ToString()}";

        public MainWindowVM()
        {
            VisitIridiumIonCommand = ReactiveCommand.CreateAsyncTask(_ => state.VisitIridiumIon());
        }
    }
}