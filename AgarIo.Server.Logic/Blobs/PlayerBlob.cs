namespace AgarIo.Server.Logic.Blobs
{
    using System;

    using AgarIo.Server.Logic.Physics;

    public class PlayerBlob : Blob
    {
        private readonly IPhysics _physics;

        public PlayerBlob(Player owner, IGame game, IPhysics physics, IStateTracker stateTracker, Vector position, bool controlledByPlayer)
            : base(game, physics, stateTracker, position, game.Settings.MinPlayerBlobMass)
        {
            _physics = physics;
            Owner = owner;
            RecombineTicksInstant = game.TickCount;
            ControlledByPlayer = controlledByPlayer;

            Mass = Game.Settings.MinPlayerBlobMass;

            MakeDynamic();
        }

        public bool ControlledByPlayer { get; private set; }

        public Player Owner { get; }

        public ulong RecombineTicksInstant { get; private set; }

        public void UpdateRecombineInstant()
        {
            RecombineTicksInstant = Game.TickCount + Game.Settings.RecombineWaitTicks +
                (ulong)(Mass * Game.Settings.RecombineWaitTimeMassFactor / Logic.Game.TickDurationMs);
        }

        public override float Mass
        {
            get { return base.Mass; }

            internal set
            {
                if (value > Game.Settings.MaxPlayerBlobMass)
                {
                    base.Mass = Game.Settings.MaxPlayerBlobMass;
                    return;
                }

                base.Mass = value;
            }
        }

        internal override bool OnCollision(Blob otherBlob)
        {
            var foodBlob = otherBlob as FoodBlob;
            if (foodBlob != null)
            {
                TryConsume(foodBlob);
                return false;
            }

            var playerBlob = otherBlob as PlayerBlob;
            if (playerBlob != null)
            {
                if (playerBlob.Owner == Owner)
                {
                    TryMerge(playerBlob);
                    return OwnedBlobsCollides(playerBlob);
                }

                TryConsume(playerBlob);
            }

            return false;
        }

        private bool OwnedBlobsCollides(PlayerBlob playerBlob)
        {
            return Game.TickCount < playerBlob.RecombineTicksInstant || Game.TickCount < RecombineTicksInstant;
        }

        private bool CanMerge(PlayerBlob playerBlob)
        {
            if (OwnedBlobsCollides(playerBlob))
            {
                return false;
            }

            return Mass >= playerBlob.Mass;
        }

        private void TryMerge(PlayerBlob playerBlob)
        {
            if (!CanMerge(playerBlob))
            {
                return;
            }

            TryConsume(playerBlob);
        }

        private bool CanConsume(FoodBlob foodBlob)
        {
            var distance = foodBlob.Position.Dist(Position) - foodBlob.Radius;
            return distance < GetEatingRange() && Mass >= foodBlob.Mass * Game.Settings.StandardEatingMassMultiplier;
        }

        private bool CanConsume(PlayerBlob playerBlob)
        {
            var distance = playerBlob.Position.Dist(Position) - playerBlob.Radius;
            return distance < GetEatingRange() && Mass >= playerBlob.Mass * Game.Settings.StandardEatingMassMultiplier;
        }

        private void TryConsume(PlayerBlob playerBlob)
        {
            if (!CanConsume(playerBlob))
            {
                return;
            }

            Mass += playerBlob.Mass;
            Game.RemoveBlob(playerBlob);
        }

        private void TryConsume(FoodBlob foodBlob)
        {
            if (!CanConsume(foodBlob))
            {
                return;
            }

            Mass += foodBlob.Mass;
            Game.RemoveBlob(foodBlob);
        }

        internal override void Update()
        {
            if (!ControlledByPlayer)
            {
                base.Update();
            }

            Mass *= 1.0f - Game.Settings.MassLossPerTick;
            if (Mass < Game.Settings.MinPlayerBlobMass)
            {
                Mass = Game.Settings.MinPlayerBlobMass;
            }

            if (Velocity.Length < GetMaxSpeed(Mass))
            {
                ControlledByPlayer = true;
            }
        }

        internal override void OnCreate()
        {
            base.OnCreate();
            Owner.AddBlob(this);
        }

        internal override void OnRemove()
        {
            Owner.RemoveBlob(this);
            base.OnRemove();
        }

        private double GetEatingRange()
        {
            return Radius * Game.Settings.PlayerEatingRangeFactor;
        }

        internal void Split()
        {
            if (Owner.Blobs.Count >= Game.Settings.MaxPlayerBlobCount)
            {
                return;
            }

            if (Mass < Game.Settings.MinMassSplit)
            {
                return;
            }

            var newMass = Mass / 2.0f;

            Mass = newMass;

            var split = new PlayerBlob(Owner, Game, _physics, StateTracker, Position, false)
            {
                Mass = newMass,
                Velocity = Velocity * Game.Settings.SplitSpeedFactor
            };

            split.UpdateRecombineInstant();
            UpdateRecombineInstant();

            Game.AddBlob(split);
        }

        internal void EjectMass()
        {
            if (Mass < Game.Settings.MinMassEject)
            {
                return;
            }

            var ejectMass = Game.Settings.EjectMass;
            var normalizedVelocity = Velocity.Normalize();
            var ejectVelocity = normalizedVelocity * Game.Settings.EjectSpeed;

            Mass -= ejectMass;

            var ejectedMassBlobPosition = Position + (normalizedVelocity * (Radius + GetRadius(ejectMass)));

            var ejectedMassBlob = new EjectedMassBlob(Game, _physics, StateTracker, ejectedMassBlobPosition)
            {
                Mass = ejectMass,
                Velocity = ejectVelocity
            };

            Game.AddBlob(ejectedMassBlob);
        }

        public static float GetMaxSpeed(float mass)
        {
            return 120.0f * (float)Math.Pow(mass, -1.0f / 4.5f) * Logic.Game.TickDurationMs / 40.0f;
        }
    }
}