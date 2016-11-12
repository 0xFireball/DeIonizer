using Avalonia.Threading;
using DeIonizer.Core.Flooders;
using DeIonizer.States;
using ReactiveUI;
using System;
using System.Net;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace DeIonizer.SharedUI.VM
{
    internal class MainWindowVM : ReactiveObject
    {
        private readonly static string s_defaultStatus = "Ready";
        private readonly static string s_attackingStatus = "Attacking";
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

        public bool SelectedAttackAvailable => (SelectedAttack == null) || !(SelectedAttack.IsFlooding);

        public int AttackThreadCount { get; set; } = 4;

        public int AttackPort { get; set; } = 80;

        public int AttackPacketDelay { get; set; } = 10;

        public int? RequestedPackets => SelectedAttack?.Requested;

        public int? FailedPackets => SelectedAttack?.Failed;

        public string AttackMessage { get; set; } = "U dun goofed";

        public string StatusText { get; private set; } = s_defaultStatus;

        public string ControlButtonText => _attackStatus.HasValue && _attackStatus.Value ? s_stopAttackText : s_startAttackText;

        public MainWindowVM()
        {
            VisitIridiumIonCommand = ReactiveCommand.CreateAsyncTask(_ => state.VisitIridiumIon());
            LockTargetCommand = ReactiveCommand.CreateAsyncTask(_ => LockTarget());
            ActivateControlRunnerCommand = ReactiveCommand.CreateAsyncTask(_ => ActivateControlRunner());
            this.WhenAnyValue(x => x.RequestedPackets)
                .Subscribe(x => UpdateStatistics());
        }

        private async Task ActivateControlRunner()
        {
            if (SelectedAttack == null) return;
            if (LockedTargetAddress == null) return;
            //Register event
            SelectedAttack.FloodError += HandleFlooderError;
            //Update parameters
            SelectedAttack.Delay = AttackPacketDelay;
            SelectedAttack.Threads = AttackThreadCount;
            SelectedAttack.Port = AttackPort;
            SelectedAttack.Message = AttackMessage;
            SelectedAttack.Target = IPAddress.Parse(LockedTargetAddress);
            if (SelectedAttack.IsFlooding)
            {
                SelectedAttack.Stop();
                UpdateStatus(s_defaultStatus);
                SelectedAttack.Reset();
            }
            else
            {
                SelectedAttack.Start();
                UpdateStatus(s_attackingStatus);
                //Schedule UI updates
                var updaterResult = Task.Factory.StartNew(() =>
                {
                    while (SelectedAttack.IsFlooding)
                    {
                        UpdateStatistics();
                        Thread.Sleep(200);
                    }
                });
            }
            RefreshAttackUi();
        }

        private void RefreshAttackUi()
        {
            this.RaisePropertyChanged(nameof(SelectedAttackAvailable));
            this.RaisePropertyChanged(nameof(ControlButtonText));
            this.RaisePropertyChanged(nameof(RequestedPackets));
            this.RaisePropertyChanged(nameof(FailedPackets));
        }

        private async Task LockTarget()
        {
            UpdateStatus("Resolving Target...");
            var resolvedAddress = await state.LockTarget(TargetLocation) ?? "Unable to resolve";
            LockedTargetAddress = resolvedAddress;
            this.RaisePropertyChanged(nameof(LockedTargetAddress));
            UpdateStatus(s_defaultStatus);
        }

        private void UpdateStatus(string newValue)
        {
            StatusText = newValue;
            this.RaisePropertyChanged(nameof(StatusText));
        }

        public void UpdateStatistics()
        {
            if (_attackStatus.HasValue && _attackStatus.Value)
            {
                //We're attacking
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(RequestedPackets));
                    this.RaisePropertyChanged(nameof(FailedPackets));
                });
            }
        }

        private void HandleFlooderError(object sender, EventArgs e)
        {
            //Update UI
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                this.RaisePropertyChanged(nameof(SelectedAttack));

                //Safe cleanup
                SelectedAttack.Stop();
                UpdateStatus(s_defaultStatus);
                RefreshAttackUi();
            });
        }
    }
}