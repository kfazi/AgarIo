namespace AgarIo.AdminPanel.ViewModels
{
    using AgarIo.Contract;

    using Caliburn.Micro;

    public class SettingsViewModel : Screen
    {
        public SettingsViewModel()
        {
            MinPlayerBlobMass = new UpdateableFieldViewModel<float>();
            MaxPlayerBlobMass = new UpdateableFieldViewModel<float>();
            VisibilityFactor = new UpdateableFieldViewModel<float>();
            MassGainFactor = new UpdateableFieldViewModel<float>();
            RecombineWaitTimeMassFactor = new UpdateableFieldViewModel<float>();
            MassLossPerTick = new UpdateableFieldViewModel<float>();
            MoveDecayPerTick = new UpdateableFieldViewModel<float>();
            PlayerEatingRangeFactor = new UpdateableFieldViewModel<float>();
            VirusEatingRangeFactor = new UpdateableFieldViewModel<float>();
            SplitSpeedFactor = new UpdateableFieldViewModel<float>();
            VirusMassMultiplier = new UpdateableFieldViewModel<float>();
            StandardEatingMassMultiplier = new UpdateableFieldViewModel<float>();
            MaxPlayerBlobCount = new UpdateableFieldViewModel<int>();
            FoodSpawnAmount = new UpdateableFieldViewModel<int>();
            MaxFoodCount = new UpdateableFieldViewModel<int>();
            FoodMass = new UpdateableFieldViewModel<int>();
            VirusMinAmount = new UpdateableFieldViewModel<int>();
            VirusMaxAmount = new UpdateableFieldViewModel<int>();
            VirusMinMass = new UpdateableFieldViewModel<int>();
            VirusFeedAmount = new UpdateableFieldViewModel<int>();
            EjectMass = new UpdateableFieldViewModel<int>();
            EjectMassLoss = new UpdateableFieldViewModel<int>();
            EjectSpeed = new UpdateableFieldViewModel<int>();
            MinMassEject = new UpdateableFieldViewModel<int>();
            MinMassSplit = new UpdateableFieldViewModel<int>();
            InitialFoodAmount = new UpdateableFieldViewModel<int>();
            RecombineWaitTicks = new UpdateableFieldViewModel<uint>();
            FoodSpawnTicksInterval = new UpdateableFieldViewModel<uint>();
            VirusSpawnTicksInterval = new UpdateableFieldViewModel<uint>();

            MinPlayerBlobMass.AddValidationRule(x => x.Value)
                .Condition(x => x.Value > 0)
                .Message("MinPlayerBlobMass must be positive");
            MaxPlayerBlobMass.AddValidationRule(x => x.Value)
                .Condition(x => x.Value > 0)
                .Message("MaxPlayerBlobMass must be positive");
            VisibilityFactor.AddValidationRule(x => x.Value)
               .Condition(x => x.Value > 0)
               .Message("VisibilityFactor must be positive");
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

        public UpdateableFieldViewModel<uint> RecombineWaitTicks { get; }

        public UpdateableFieldViewModel<uint> FoodSpawnTicksInterval { get; }

        public UpdateableFieldViewModel<uint> VirusSpawnTicksInterval { get; }

        public void UpdateFromServer(WorldSettingsDto worldSettingsDto)
        {
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
            RecombineWaitTicks.OriginalValue = worldSettingsDto.RecombineWaitTicks;
            FoodSpawnTicksInterval.OriginalValue = worldSettingsDto.FoodSpawnTicksInterval;
            VirusSpawnTicksInterval.OriginalValue = worldSettingsDto.VirusSpawnTicksInterval;
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
                RecombineWaitTicks = RecombineWaitTicks.Value,
                FoodSpawnTicksInterval = FoodSpawnTicksInterval.Value,
                VirusSpawnTicksInterval = VirusSpawnTicksInterval.Value
            };
        }
    }
}