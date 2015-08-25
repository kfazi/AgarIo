namespace AgarIo.Contract
{
    public class WorldSettingsDto
    {
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

        public uint RecombineWaitTicks { get; set; }

        public uint FoodSpawnTicksInterval { get; set; }

        public uint VirusSpawnTicksInterval { get; set; }
    }
}