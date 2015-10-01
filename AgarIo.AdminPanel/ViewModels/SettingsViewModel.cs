namespace AgarIo.AdminPanel.ViewModels
{
    using System.Threading;
    using System.Threading.Tasks;

    using AgarIo.AdminPanel.Events;
    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;

    using Caliburn.Micro;

    public class SettingsViewModel : Screen, IHandle<SnapshotEvent>
    {
        private readonly IConnection _connection;

        private bool _isGameRunning;

        public SettingsViewModel(IEventAggregator eventAggregator, IConnection connection)
        {
            _connection = connection;
            MinPlayerBlobMass = new UpdateableFieldViewModel<float>(10.0f);
            MaxPlayerBlobMass = new UpdateableFieldViewModel<float>(2500.0f);
            VisibilityFactor = new UpdateableFieldViewModel<float>(10.0f);
            MassGainFactor = new UpdateableFieldViewModel<float>(0.8f);
            RecombineWaitTimeMassFactor = new UpdateableFieldViewModel<float>(0.02f);
            MassLossPerTick = new UpdateableFieldViewModel<float>(0.001f);
            MoveDecayPerTick = new UpdateableFieldViewModel<float>(0.75f);
            PlayerEatingRangeFactor = new UpdateableFieldViewModel<float>(0.8f);
            VirusEatingRangeFactor = new UpdateableFieldViewModel<float>(0.4f);
            SplitSpeedFactor = new UpdateableFieldViewModel<float>(6.0f);
            VirusMassMultiplier = new UpdateableFieldViewModel<float>(1.33f);
            StandardEatingMassMultiplier = new UpdateableFieldViewModel<float>(1.25f);
            MaxPlayerBlobCount = new UpdateableFieldViewModel<int>(8);
            FoodSpawnAmount = new UpdateableFieldViewModel<int>(10);
            MaxFoodCount = new UpdateableFieldViewModel<int>(1000);
            FoodMass = new UpdateableFieldViewModel<int>(1);
            VirusMinAmount = new UpdateableFieldViewModel<int>(10);
            VirusMaxAmount = new UpdateableFieldViewModel<int>(20);
            VirusMinMass = new UpdateableFieldViewModel<int>(100);
            VirusFeedAmount = new UpdateableFieldViewModel<int>(7);
            EjectMass = new UpdateableFieldViewModel<int>(12);
            EjectMassLoss = new UpdateableFieldViewModel<int>(16);
            EjectSpeed = new UpdateableFieldViewModel<int>(1600);
            MinMassEject = new UpdateableFieldViewModel<int>(32);
            MinMassSplit = new UpdateableFieldViewModel<int>(36);
            InitialFoodAmount = new UpdateableFieldViewModel<int>(100);
            VirusAbsoluteMaxAmount = new UpdateableFieldViewModel<int>(30);
            RecombineWaitTicks = new UpdateableFieldViewModel<uint>(600);
            FoodSpawnTicksInterval = new UpdateableFieldViewModel<uint>(2);
            VirusSpawnTicksInterval = new UpdateableFieldViewModel<uint>(2);
            EjectMassWaitTicks = new UpdateableFieldViewModel<uint>(10);
            SplitWaitTicks = new UpdateableFieldViewModel<uint>(10);

            WorldSize = new UpdateableFieldViewModel<int>(2000);

            MinPlayerBlobMass.AddValidationRule(x => x.Value)
                .Condition(x => x.Value > 0)
                .Message("MinPlayerBlobMass must be positive");
            MaxPlayerBlobMass.AddValidationRule(x => x.Value)
                .Condition(x => x.Value > 0)
                .Message("MaxPlayerBlobMass must be positive");
            VisibilityFactor.AddValidationRule(x => x.Value)
               .Condition(x => x.Value > 0)
               .Message("VisibilityFactor must be positive");

            eventAggregator.Subscribe(this);
        }

        public UpdateableFieldViewModel<float> MinPlayerBlobMass { get; }

        public UpdateableFieldViewModel<float> MaxPlayerBlobMass { get; }

        public UpdateableFieldViewModel<float> VisibilityFactor { get; }

        public UpdateableFieldViewModel<float> MassGainFactor { get; }

        public UpdateableFieldViewModel<float> RecombineWaitTimeMassFactor { get; }

        public UpdateableFieldViewModel<float> MassLossPerTick { get; }

        public UpdateableFieldViewModel<float> MoveDecayPerTick { get; }

        public UpdateableFieldViewModel<float> PlayerEatingRangeFactor { get; }

        public UpdateableFieldViewModel<float> VirusEatingRangeFactor { get; }

        public UpdateableFieldViewModel<float> SplitSpeedFactor { get; }

        public UpdateableFieldViewModel<float> VirusMassMultiplier { get; }

        public UpdateableFieldViewModel<float> StandardEatingMassMultiplier { get; }

        public UpdateableFieldViewModel<int> MaxPlayerBlobCount { get; }

        public UpdateableFieldViewModel<int> FoodSpawnAmount { get; }

        public UpdateableFieldViewModel<int> MaxFoodCount { get; }

        public UpdateableFieldViewModel<int> FoodMass { get; }

        public UpdateableFieldViewModel<int> VirusMinAmount { get; }

        public UpdateableFieldViewModel<int> VirusMaxAmount { get; }

        public UpdateableFieldViewModel<int> VirusMinMass { get; }

        public UpdateableFieldViewModel<int> VirusFeedAmount { get; }

        public UpdateableFieldViewModel<int> EjectMass { get; }

        public UpdateableFieldViewModel<int> EjectMassLoss { get; }

        public UpdateableFieldViewModel<int> EjectSpeed { get; }

        public UpdateableFieldViewModel<int> MinMassEject { get; }

        public UpdateableFieldViewModel<int> MinMassSplit { get; }

        public UpdateableFieldViewModel<int> InitialFoodAmount { get; }

        public UpdateableFieldViewModel<int> VirusAbsoluteMaxAmount { get; }

        public UpdateableFieldViewModel<uint> RecombineWaitTicks { get; }

        public UpdateableFieldViewModel<uint> FoodSpawnTicksInterval { get; }

        public UpdateableFieldViewModel<uint> VirusSpawnTicksInterval { get; }

        public UpdateableFieldViewModel<uint> EjectMassWaitTicks { get; }

        public UpdateableFieldViewModel<uint> SplitWaitTicks { get; }

        public UpdateableFieldViewModel<int> WorldSize { get; }

        public bool IsGameRunning
        {
            get { return _isGameRunning; }

            set
            {
                if (value == _isGameRunning)
                {
                    return;
                }

                _isGameRunning = value;
                NotifyOfPropertyChange(() => IsGameRunning);
            }
        }

        public WorldSettingsDto GetWorldSettingsDto()
        {
            return new WorldSettingsDto
            {
                MinPlayerBlobMass = MinPlayerBlobMass.Value,
                MaxPlayerBlobMass = MaxPlayerBlobMass.Value,
                VisibilityFactor = VisibilityFactor.Value,
                MassGainFactor = MassGainFactor.Value,
                RecombineWaitTimeMassFactor = RecombineWaitTimeMassFactor.Value,
                MassLossPerTick = MassLossPerTick.Value,
                MoveDecayPerTick = MoveDecayPerTick.Value,
                PlayerEatingRangeFactor = PlayerEatingRangeFactor.Value,
                VirusEatingRangeFactor = VirusEatingRangeFactor.Value,
                SplitSpeedFactor = SplitSpeedFactor.Value,
                VirusMassMultiplier = VirusMassMultiplier.Value,
                StandardEatingMassMultiplier = StandardEatingMassMultiplier.Value,
                MaxPlayerBlobCount = MaxPlayerBlobCount.Value,
                FoodSpawnAmount = FoodSpawnAmount.Value,
                MaxFoodCount = MaxFoodCount.Value,
                FoodMass = FoodMass.Value,
                VirusMinAmount = VirusMinAmount.Value,
                VirusMaxAmount = VirusMaxAmount.Value,
                VirusMinMass = VirusMinMass.Value,
                VirusFeedAmount = VirusFeedAmount.Value,
                EjectMass = EjectMass.Value,
                EjectMassLoss = EjectMassLoss.Value,
                EjectSpeed = EjectSpeed.Value,
                MinMassEject = MinMassEject.Value,
                MinMassSplit = MinMassSplit.Value,
                InitialFoodAmount = InitialFoodAmount.Value,
                VirusAbsoluteMaxAmount = VirusAbsoluteMaxAmount.Value,
                RecombineWaitTicks = RecombineWaitTicks.Value,
                FoodSpawnTicksInterval = FoodSpawnTicksInterval.Value,
                VirusSpawnTicksInterval = VirusSpawnTicksInterval.Value,
                EjectMassWaitTicks = EjectMassWaitTicks.Value,
                SplitWaitTicks = SplitWaitTicks.Value
            };
        }

        public void Handle(SnapshotEvent message)
        {
            var snapshot = message.Snapshot;
            var worldSettingsDto = message.Snapshot.WorldSettings;
            MinPlayerBlobMass.OriginalValue = worldSettingsDto.MinPlayerBlobMass;
            MaxPlayerBlobMass.OriginalValue = worldSettingsDto.MaxPlayerBlobMass;
            VisibilityFactor.OriginalValue = worldSettingsDto.VisibilityFactor;
            MassGainFactor.OriginalValue = worldSettingsDto.MassGainFactor;
            RecombineWaitTimeMassFactor.OriginalValue = worldSettingsDto.RecombineWaitTimeMassFactor;
            MassLossPerTick.OriginalValue = worldSettingsDto.MassLossPerTick;
            MoveDecayPerTick.OriginalValue = worldSettingsDto.MoveDecayPerTick;
            PlayerEatingRangeFactor.OriginalValue = worldSettingsDto.PlayerEatingRangeFactor;
            VirusEatingRangeFactor.OriginalValue = worldSettingsDto.VirusEatingRangeFactor;
            SplitSpeedFactor.OriginalValue = worldSettingsDto.SplitSpeedFactor;
            VirusMassMultiplier.OriginalValue = worldSettingsDto.VirusMassMultiplier;
            StandardEatingMassMultiplier.OriginalValue = worldSettingsDto.StandardEatingMassMultiplier;
            MaxPlayerBlobCount.OriginalValue = worldSettingsDto.MaxPlayerBlobCount;
            FoodSpawnAmount.OriginalValue = worldSettingsDto.FoodSpawnAmount;
            MaxFoodCount.OriginalValue = worldSettingsDto.MaxFoodCount;
            FoodMass.OriginalValue = worldSettingsDto.FoodMass;
            VirusMinAmount.OriginalValue = worldSettingsDto.VirusMinAmount;
            VirusMaxAmount.OriginalValue = worldSettingsDto.VirusMaxAmount;
            VirusMinMass.OriginalValue = worldSettingsDto.VirusMinMass;
            VirusFeedAmount.OriginalValue = worldSettingsDto.VirusFeedAmount;
            EjectMass.OriginalValue = worldSettingsDto.EjectMass;
            EjectMassLoss.OriginalValue = worldSettingsDto.EjectMassLoss;
            EjectSpeed.OriginalValue = worldSettingsDto.EjectSpeed;
            MinMassEject.OriginalValue = worldSettingsDto.MinMassEject;
            MinMassSplit.OriginalValue = worldSettingsDto.MinMassSplit;
            InitialFoodAmount.OriginalValue = worldSettingsDto.InitialFoodAmount;
            VirusAbsoluteMaxAmount.OriginalValue = worldSettingsDto.VirusAbsoluteMaxAmount;
            RecombineWaitTicks.OriginalValue = worldSettingsDto.RecombineWaitTicks;
            FoodSpawnTicksInterval.OriginalValue = worldSettingsDto.FoodSpawnTicksInterval;
            VirusSpawnTicksInterval.OriginalValue = worldSettingsDto.VirusSpawnTicksInterval;
            EjectMassWaitTicks.OriginalValue = worldSettingsDto.EjectMassWaitTicks;
            SplitWaitTicks.OriginalValue = worldSettingsDto.SplitWaitTicks;
            if (snapshot.WorldSize != 0)
            {
                WorldSize.OriginalValue = snapshot.WorldSize;
            }

            IsGameRunning = snapshot.IsGameRunning;
        }

        public async Task SendToServerAsync()
        {
            await _connection.DispatchCommandAsync<AdminCommandResponseDto>(
                new UpdateSettingsAdminCommandDto { Settings = GetWorldSettingsDto() },
                CancellationToken.None);
        }

        public async Task StartGameAsync()
        {
            await SendToServerAsync();
            await _connection.DispatchCommandAsync<AdminCommandResponseDto>(
                new StartGameAdminCommandDto { Size = WorldSize.Value },
                CancellationToken.None);
        }

        public async Task StopGameAsync()
        {
            await _connection.DispatchCommandAsync<AdminCommandResponseDto>(
                new StopGameAdminCommandDto(),
                CancellationToken.None);
        }
    }
}