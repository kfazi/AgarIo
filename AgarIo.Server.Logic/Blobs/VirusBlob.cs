namespace AgarIo.Server.Logic.Blobs
{
    using System;

    using AgarIo.Server.Logic.Physics;

    public class VirusBlob : Blob
    {
        private readonly IPhysics _physics;

        private int _fed;

        public VirusBlob(IGame game, IPhysics physics, IStateTracker stateTracker, Vector position)
            : base(game, physics, stateTracker, position, game.Settings.VirusMinMass)
        {
            _physics = physics;

            _fed = 0;

            MakeStatic();
        }

        public double GetEatingRange()
        {
            return Radius * Game.Settings.VirusEatingRangeFactor;
        }

        public double GetMinConsumerMass()
        {
            return Mass * Game.Settings.VirusMassMultiplier;
        }

        internal override bool OnCollision(Blob otherBlob)
        {
            var ejectedMassBlob = otherBlob as EjectedMassBlob;
            if (ejectedMassBlob != null)
            {
                TryConsume(ejectedMassBlob);
                return false;
            }

            var playerBlob = otherBlob as PlayerBlob;
            if (playerBlob != null)
            {
                TryConsume(playerBlob);
            }

            return false;
        }

        private void TryConsume(EjectedMassBlob blob)
        {
            if (!CanConsume(blob))
            {
                return;
            }

            Mass += blob.Mass;
            _fed++;

            if (_fed < Game.Settings.VirusFeedAmount)
            {
                return;
            }

            var normalizedVelocity = blob.Velocity.Normalize();
            var virus = new VirusBlob(Game, _physics, StateTracker, Position + normalizedVelocity)
            {
                Velocity = normalizedVelocity * 200
            };

            Game.AddBlob(virus);
        }

        private bool CanConsume(EjectedMassBlob blob)
        {
            var distance = blob.Position.Dist(Position) - blob.Radius;
            return distance < GetEatingRange();
        }

        private bool CanConsume(PlayerBlob playerBlob)
        {
            var distance = playerBlob.Position.Dist(Position) - playerBlob.Radius;
            return distance < GetEatingRange() && playerBlob.Mass >= GetMinConsumerMass();
        }

        private void TryConsume(PlayerBlob blob)
        {
            if (!CanConsume(blob))
            {
                return;
            }

            var maxSplits = (int)Math.Floor(blob.Mass / Game.Settings.MaxPlayerBlobCount);
            var numSplits = Game.Settings.MaxPlayerBlobCount - blob.Owner.Blobs.Count;

            numSplits = Math.Min(numSplits, maxSplits);

            var splitMass = Math.Min(blob.Mass / (numSplits + 1), Game.Settings.MinMassSplit);

            blob.Mass += Mass;
            Game.RemoveBlob(this);

            if (numSplits <= 0)
            {
                return;
            }

            var bigSplits = 0;
            var endMass = blob.Mass - (numSplits * splitMass);
            if ((endMass > 300) && (numSplits > 0))
            {
                bigSplits++;
                numSplits--;
            }
            if ((endMass > 1200) && (numSplits > 0))
            {
                bigSplits++;
                numSplits--;
            }
            if ((endMass > 3000) && (numSplits > 0))
            {
                bigSplits++;
                numSplits--;
            }

            for (var i = 0; i < numSplits; i++)
            {
                var direction = GetVectorInDirection(2.0 * Math.PI * (i / (double)numSplits));
                SplitConsumer(blob, splitMass, direction);
                blob.Mass -= splitMass;
            }

            for (var i = 0; i < bigSplits; i++)
            {
                var direction = GetVectorInDirection(Game.Random.NextDouble() * 2 * Math.PI);
                splitMass = blob.Mass / 4;
                SplitConsumer(blob, splitMass, direction);
                blob.Mass -= splitMass;
            }

            blob.UpdateRecombineInstant();
        }

        private void SplitConsumer(PlayerBlob consumer, float splitMass, Vector direction)
        {
            var speed = consumer.Velocity.Length * Game.Settings.SplitSpeedFactor;
            var split = new PlayerBlob(consumer.Owner, Game, _physics, StateTracker, consumer.Position, false)
            {
                Velocity = direction * speed,
                Mass = splitMass
            };
            
            split.UpdateRecombineInstant();

            Game.AddBlob(split);
        }

        private static Vector GetVectorInDirection(double radians)
        {
            return new Vector((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
}