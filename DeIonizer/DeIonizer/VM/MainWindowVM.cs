using DeIonizer.States;
using ReactiveUI;
using System.Reactive;
using System;
using System.Threading.Tasks;

namespace DeIonizer.VM
{
    internal class MainWindowVM : ReactiveObject
    {
        private readonly MainWindowState state = new MainWindowState();

        public ReactiveCommand<Unit> VisitIridiumIonCommand { get; }

        public ReactiveCommand<Unit> LockTargetCommand { get; }

        public string ApplicationVersion => $"v{typeof(MainWindowVM).Assembly.GetName().Version.ToString()}";

        public string TargetLocation { get; set; }

        public MainWindowVM()
        {
            VisitIridiumIonCommand = ReactiveCommand.CreateAsyncTask(_ => state.VisitIridiumIon());
            LockTargetCommand = ReactiveCommand.CreateAsyncTask(_ => LockTarget());
        }

        private async Task LockTarget()
        {
            await state.LockTarget(TargetLocation);
        }
    }
}