using DeIonizer.Core.Flooders;
using DeIonizer.States;
using ReactiveUI;
using System.Reactive;
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

        public string LockedTargetAddress { get; private set; }

        public ReactiveList<IFlooder> AvailableAttacks { get; private set; } = new ReactiveList<IFlooder>(FlooderLoaderService.BuiltinFlooders);

        public MainWindowVM()
        {
            VisitIridiumIonCommand = ReactiveCommand.CreateAsyncTask(_ => state.VisitIridiumIon());
            LockTargetCommand = ReactiveCommand.CreateAsyncTask(_ => LockTarget());
        }

        private async Task LockTarget()
        {
            var resolvedAddress = await state.LockTarget(TargetLocation) ?? "Unable to resolve";
            LockedTargetAddress = resolvedAddress;
            this.RaisePropertyChanged(nameof(LockedTargetAddress));
        }
    }
}