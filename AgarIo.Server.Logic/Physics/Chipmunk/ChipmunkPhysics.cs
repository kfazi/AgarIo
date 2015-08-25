namespace AgarIo.Server.Logic.Physics.Chipmunk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AgarIo.Server.Logic.Blobs;

    using ChipmunkSharp;

    using NLog;

    public class ChipmunkPhysics : IPhysics
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static readonly object UpdateLock = new object();

        private static readonly object ShapesListLock = new object();

        private readonly List<cpShape> _shapesToAdd;

        private readonly List<cpShape> _shapesToRemove;

        private cpSpace _space;

        private int _worldSize;

        private bool _reindexStatic;

        public ChipmunkPhysics()
        {
            _shapesToAdd = new List<cpShape>();
            _shapesToRemove = new List<cpShape>();
        }

        public void Start(int worldSize)
        {
            lock (UpdateLock)
            {
                _worldSize = worldSize;

                _space = new cpSpace { CollisionEnabled = true };
                _space.SetGravity(cpVect.Zero);

                var boxBody = cpBody.NewStatic();

                var leftShape = new cpSegmentShape(boxBody, new cpVect(-worldSize, -worldSize), new cpVect(-worldSize, worldSize), 5);
                var topShape = new cpSegmentShape(boxBody, new cpVect(-worldSize, worldSize), new cpVect(worldSize, worldSize), 5);
                var rightShape = new cpSegmentShape(boxBody, new cpVect(worldSize, worldSize), new cpVect(worldSize, -worldSize), 5);
                var bottomShape = new cpSegmentShape(boxBody, new cpVect(worldSize, -worldSize), new cpVect(-worldSize, -worldSize), 5);

                leftShape.SetCollisionType(GenerateCollisionType<cpSpace>());
                topShape.SetCollisionType(GenerateCollisionType<cpSpace>());
                rightShape.SetCollisionType(GenerateCollisionType<cpSpace>());
                bottomShape.SetCollisionType(GenerateCollisionType<cpSpace>());

                _space.AddShape(leftShape);
                _space.AddShape(topShape);
                _space.AddShape(rightShape);
                _space.AddShape(bottomShape);

                var defaultCollisionHandler = _space.AddDefaultCollisionHandler();
                defaultCollisionHandler.preSolveFunc = (arbiter, space, userData) => false;

                var playerCollisionHandler = _space.AddCollisionHandler(GenerateCollisionType<PlayerBlob>(), GenerateCollisionType<PlayerBlob>());
                playerCollisionHandler.preSolveFunc = OnPreSolve;

                _reindexStatic = true;
            }
        }

        public void Stop()
        {
            lock (UpdateLock)
            {
                _shapesToAdd.Clear();
                _shapesToRemove.Clear();
            }
        }

        public void ForBlobsInRange(Vector position, float radius, Action<Blob> action)
        {
            lock (UpdateLock)
            {
                var boundingBox = cpBB.cpBBNewForCircle(ChipmunkVectorConverter.ToChipmunk(position), radius);
                _space.BBQuery(
                    boundingBox, cpShape.FILTER_ALL, (shape, data) =>
                    {
                        var blob = shape?.body.GetUserData() as Blob;
                        if (blob == null)
                        {
                            return;
                        }

                        action(blob);
                    }, null);
            }
        }

        public IBody CreateBody(Blob blob, float radius, float mass, bool isStatic)
        {
            lock (UpdateLock)
            {
                var body = isStatic ? cpBody.NewStatic() : cpBody.New(mass, float.PositiveInfinity);
                body.SetUserData(blob);

                var shape = new cpCircleShape(body, radius, cpVect.Zero);
                shape.SetCollisionType(GenerateCollisionType(blob.GetType()));

                lock (ShapesListLock)
                {
                    _shapesToAdd.Add(shape);
                }

                if (_space.IsLocked)
                {
                    _space.AddPostStepCallback(OnPostStep, null, null);
                }
                else
                {
                    AddShapes();
                }

                _reindexStatic |= isStatic;

                return new ChipmunkBody(shape, isStatic);
            }
        }

        public void DestroyBody(IBody body)
        {
            lock (UpdateLock)
            {
                var chipmunkBody = (ChipmunkBody)body;

                lock (ShapesListLock)
                {
                    _shapesToRemove.Add(chipmunkBody.Instance);
                }

                if (_space.IsLocked)
                {
                    _space.AddPostStepCallback(OnPostStep, null, null);
                }
                else
                {
                    RemoveShapes();
                }
            }
        }

        public void Update()
        {
            lock (UpdateLock)
            {
                var tries = 5;
                try
                {
                    if (_reindexStatic)
                    {
                        _space.ReindexStatic();
                        _reindexStatic = false;
                    }

                    _space.Step(Game.TickDurationMs * 0.001f);

                    AssureLimits();
                    RecheckCollisions();
                }
                catch (Exception exception)
                {
                    Log.Error(exception);
                    tries--;
                    if (tries <= 0)
                    {
                        throw;
                    }
                }
            }
        }

        public IBody MakeBodyStatic(IBody body)
        {
            var chipmunkBody = (ChipmunkBody)body;
            var mass = chipmunkBody.Mass;
            var radius = chipmunkBody.Radius;
            var position = chipmunkBody.Position;
            var blob = (Blob)chipmunkBody.Instance.GetUserData();
            DestroyBody(body);
            var staticBody = CreateBody(blob, radius, mass, true);
            staticBody.Position = position;
            return staticBody;
        }

        public IBody MakeBodyDynamic(IBody body)
        {
            var chipmunkBody = (ChipmunkBody)body;
            var mass = chipmunkBody.Mass;
            var radius = chipmunkBody.Radius;
            var position = chipmunkBody.Position;
            var velocity = chipmunkBody.LinearVelocity;
            var blob = (Blob)chipmunkBody.Instance.GetUserData();
            DestroyBody(body);
            var dynamicBody = CreateBody(blob, radius, mass, false);
            dynamicBody.Position = position;
            dynamicBody.LinearVelocity = velocity;
            return dynamicBody;
        }

        private static ulong GenerateCollisionType<T>()
        {
            return GenerateCollisionType(typeof(T));
        }

        private static ulong GenerateCollisionType(Type type)
        {
            return unchecked((ulong)type.GetHashCode());
        }

        private static bool OnPreSolve(cpArbiter arbiter, cpSpace space, object userData)
        {
            var firstBlob = arbiter.body_a.GetUserData() as Blob;
            var secondBlob = arbiter.body_b.GetUserData() as Blob;

            if (firstBlob == null || secondBlob == null)
            {
                return true;
            }

            var firstCollisionResult = firstBlob.OnCollision(secondBlob);
            var secondCollisionResult = secondBlob.OnCollision(firstBlob);

            return firstCollisionResult || secondCollisionResult;
        }

        private void OnPostStep(cpSpace cpSpace, object key, object data)
        {
            AddShapes();
            RemoveShapes();
        }

        private void AddShapes()
        {
            lock (ShapesListLock)
            {
                foreach (var shape in _shapesToAdd)
                {
                    _space.AddShape(shape);
                    if (!_space.ContainsBody(shape.body))
                    {
                        _space.AddBody(shape.body);
                    }
                }

                _shapesToAdd.Clear();
            }
        }

        private void RemoveShapes()
        {
            lock (ShapesListLock)
            {
                foreach (var shape in _shapesToRemove)
                {
                    _space.RemoveShape(shape);
                    _space.RemoveBody(shape.body);
                }

                _shapesToRemove.Clear();
            }
        }

        private void AssureLimits()
        {
            foreach (var body in _space.dynamicBodies)
            {
                body.Activate();

                var position = body.GetPosition();
                if (position.x >= -_worldSize && position.x <= _worldSize && position.y >= -_worldSize && position.y <= _worldSize)
                {
                    continue;
                }

                position.x = Math.Min(Math.Max(position.x, -_worldSize + 1), _worldSize - 1);
                position.y = Math.Min(Math.Max(position.y, -_worldSize + 1), _worldSize - 1);
                body.SetPosition(position);

                var velocity = body.GetVelocity();
                body.SetVelocity(new cpVect(-velocity.x, -velocity.y));
            }
        }

        private void RecheckCollisions()
        {
            var playerBlobs = _space.dynamicBodies
                .Select(body => body.GetUserData())
                .OfType<PlayerBlob>()
                .Where(blob => blob != null)
                .ToArray();
            foreach (var firstBlob in playerBlobs)
            {
                ForBlobsInRange(firstBlob.Position, firstBlob.Radius, secondBlob =>
                {
                    if (ReferenceEquals(firstBlob, secondBlob))
                    {
                        return;
                    }

                    var distance = firstBlob.Position.Dist(secondBlob.Position);
                    if (distance > firstBlob.Radius + secondBlob.Radius)
                    {
                        return;
                    }

                    firstBlob.OnCollision(secondBlob);
                    secondBlob.OnCollision(firstBlob);
                });
            }
        }
    }
}