using System;
using System.Threading;

namespace Boids.Simulation
{
    public class EntityId : IEquatable<EntityId>
    {
        private readonly int _id;

        private EntityId()
        {
            _id = EntityIdGenerator.GetNewId();
        }

        public static EntityId NewId()
        {
            return new EntityId();
        }

        public bool Equals(EntityId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((EntityId) obj);
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }

    internal static class EntityIdGenerator
    {
        private static readonly object Lock = new object();
        private static int _currentId;

        internal static int GetNewId()
        {
            lock (Lock)
            {
                Interlocked.Increment(ref _currentId);
                return _currentId;
            }
        }
    }
}