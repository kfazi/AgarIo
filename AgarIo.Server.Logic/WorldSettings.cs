namespace AgarIo.Server.Logic
{
    public class WorldSettings
    {
        private const float DefaultPlayerMinBlobMass = 10.0f;

        private const float DefaultPlayerMaxBlobMass = 2500.0f;

        private const float DefaultVisibilityFactor = 10.0f;

        private const float DefaultMassGainFactor = 0.8f;

        private const float DefaultRecombineWaitTimeMassFactor = 0.02f;

        private const float DefaultMassLossPerTick = 0.001f;

        private const float DefaultMoveDecayPerTick = 0.75f;

        private const float DefaultPlayerEatingRangeFactor = 0.7f;

        private const float DefaultVirusEatingRangeFactor = 0.4f;

        private const float DefaultSplitSpeedFactor = 6.0f;

        private const float DefaultVirusMassMultiplier = 1.33f;

        private const float DefaultStandardEatingMassMultiplier = 1.25f;

        private const int DefaultMaxPlayerBlobCount = 8;

        private const int DefaultFoodSpawnAmount = 10;

        private const int DefaultMaxFoodCount = 1000;

        private const int DefaultFoodMass = 1;

        private const int DefaultVirusMinAmount = 10;

        private const int DefaultVirusMaxAmount = 20;

        private const int DefaultVirusMinMass = 100;

        private const int DefaultVirusFeedAmount = 7;

        private const int DefaultEjectMass = 12;

        private const int DefaultEjectMassLoss = 16;

        private const int DefaultEjectSpeed = 1600;

        private const int DefaultMinMassEject = 32;

        private const int DefaultMinMassSplit = 36;

        private const int DefaultInitialFoodAmount = 100;

        private const int DefaultVirusAbsoluteMaxAmount = 30;

        private const uint DefaultRecombineWaitTicks = 600;

        private const uint DefaultFoodSpawnTicksInterval = 2;

        private const uint DefaultVirusSpawnTicksInterval = 2;

        private const uint DefaultEjectMassWaitTicks = 10;

        private const uint DefaultSplitWaitTicks = 10;

        private const uint DefaultTurnMinutes = 30;

        public WorldSettings()
        {
            MinPlayerBlobMass = DefaultPlayerMinBlobMass;
            MaxPlayerBlobMass = DefaultPlayerMaxBlobMass;
            VisibilityFactor = DefaultVisibilityFactor;
            MassGainFactor = DefaultMassGainFactor;
            RecombineWaitTimeMassFactor = DefaultRecombineWaitTimeMassFactor;
            MassLossPerTick = DefaultMassLossPerTick;
            MoveDecayPerTick = DefaultMoveDecayPerTick;
            PlayerEatingRangeFactor = DefaultPlayerEatingRangeFactor;
            VirusEatingRangeFactor = DefaultVirusEatingRangeFactor;
            SplitSpeedFactor = DefaultSplitSpeedFactor;
            VirusMassMultiplier = DefaultVirusMassMultiplier;
            StandardEatingMassMultiplier = DefaultStandardEatingMassMultiplier;
            MaxPlayerBlobCount = DefaultMaxPlayerBlobCount;
            FoodSpawnAmount = DefaultFoodSpawnAmount;
            MaxFoodCount = DefaultMaxFoodCount;
            FoodMass = DefaultFoodMass;
            VirusMinAmount = DefaultVirusMinAmount;
            VirusMaxAmount = DefaultVirusMaxAmount;
            VirusMinMass = DefaultVirusMinMass;
            VirusFeedAmount = DefaultVirusFeedAmount;
            EjectMass = DefaultEjectMass;
            EjectMassLoss = DefaultEjectMassLoss;
            EjectSpeed = DefaultEjectSpeed;
            MinMassEject = DefaultMinMassEject;
            MinMassSplit = DefaultMinMassSplit;
            InitialFoodAmount = DefaultInitialFoodAmount;
            VirusAbsoluteMaxAmount = DefaultVirusAbsoluteMaxAmount;
            RecombineWaitTicks = DefaultRecombineWaitTicks;
            FoodSpawnTicksInterval = DefaultFoodSpawnTicksInterval;
            VirusSpawnTicksInterval = DefaultVirusSpawnTicksInterval;
            EjectMassWaitTicks = DefaultEjectMassWaitTicks;
            SplitWaitTicks = DefaultSplitWaitTicks;
            TurnMinutes = DefaultTurnMinutes;
        }

        public float MinPlayerBlobMass { get; set; }

        public float MaxPlayerBlobMass { get; set; }

        public float VisibilityFactor { get; set; }

        public float MassGainFactor { get; set; }

        public float RecombineWaitTimeMassFactor { get; set; }

        public float MassLossPerTick { get; set; }

        public float MoveDecayPerTick { get; set; }

        public float PlayerEatingRangeFactor { get; set; }

        public float VirusEatingRangeFactor { get; set; }

        public float SplitSpeedFactor { get; set; }

        public float VirusMassMultiplier { get; set; }

        public float StandardEatingMassMultiplier { get; set; }

        public int MaxPlayerBlobCount { get; set; }

        public int FoodSpawnAmount { get; set; }

        public int MaxFoodCount { get; set; }

        public int FoodMass { get; set; }

        public int VirusMinAmount { get; set; }

        public int VirusMaxAmount { get; set; }

        public int VirusMinMass { get; set; }

        public int VirusFeedAmount { get; set; }

        public int EjectMass { get; set; }

        public int EjectMassLoss { get; set; }

        public int EjectSpeed { get; set; }

        public int MinMassEject { get; set; }

        public int MinMassSplit { get; set; }

        public int InitialFoodAmount { get; set; }

        public int VirusAbsoluteMaxAmount { get; set; }

        public uint RecombineWaitTicks { get; set; }

        public uint FoodSpawnTicksInterval { get; set; }

        public uint VirusSpawnTicksInterval { get; set; }

        public uint EjectMassWaitTicks { get; set; }

        public uint SplitWaitTicks { get; set; }

        public uint TurnMinutes { get; set; }
    }
}