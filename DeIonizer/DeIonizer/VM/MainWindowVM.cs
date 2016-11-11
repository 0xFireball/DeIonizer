using DeIonizer.Core.Flooders;
using DeIonizer.States;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace DeIonizer.VM
{
    internal class MainWindowVM : ReactiveObject
    {
        private readonly static string s_defaultStatus = "Ready";
        private readonly static string s_startAttackText = "Start";
        private readonly static string s_stopAttackText = "Stop";

        private readonly MainWindowState state = new MainWindowState();

        private bool? _attackStatus => SelectedAttack?.IsFlooding;

        public ReactiveCommand<Unit> VisitIridiumIonCommand { get; }

        public ReactiveCommand<Unit> LockTargetCommand { get; }

        public ReactiveCommand<Unit> ActivateControlRunnerCommand { get; }

        public string ApplicationVersion => $"v{typeof(MainWindowVM).Assembly.GetName().Version.ToString()}";

        public string TargetLocation { get; set; }

        public string LockedTargetAddress { get; private set; }

        public ReactiveList<IFlooder> AvailableAttacks { get; private set; } = new ReactiveList<IFlooder>(FlooderLoaderService.BuiltinFlooders);

        public IFlooder SelectedAttack { get; set; }

        public int AttackThreadCount { get; set; } = 4;

        public int AttackPort { get; set; } = 80;

        public string AttackMessage { get; set; } = "U dun goofed";

        public string StatusText { get; private set; } = s_defaultStatus;

        public string ControlButtonText => _attackStatus.HasValue && _attackStatus.Value ? s_stopAttackText : s_startAttackText;

        public MainWindowVM()
        {
            VisitIridiumIonCommand = ReactiveCommand.CreateAsyncTask(_ => state.VisitIridiumIon());
            LockTargetCommand = ReactiveCommand.CreateAsyncTask(_ => LockTarget());
            ActivateControlRunnerCommand = ReactiveCommand.CreateAsyncTask(_ => ActivateControlRunner());
        }

        private async Task ActivateControlRunner()
        {
            if (SelectedAttack == null) return;
            
        }

        private async Task LockTarget()
        {
            StatusText = "Resolving Target...";
            this.RaisePropertyChanged(nameof(StatusText));
            var resolvedAddress = await state.LockTarget(TargetLocation) ?? "Unable to resolve";
            LockedTargetAddress = resolvedAddress;
            this.RaisePropertyChanged(nameof(LockedTargetAddress));
            StatusText = s_defaultStatus;
            this.RaisePropertyChanged(nameof(StatusText));
        }
    }
}