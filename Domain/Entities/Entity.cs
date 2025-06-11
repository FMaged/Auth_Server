using Domain.Events;



namespace Domain.Entities
{
    public abstract class Entity<TId>: IEquatable<Entity<TId>> where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public TId Id { get; set; }
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        protected Entity(TId id) => Id = id;    

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        public override bool Equals(object? obj)
        => obj is Entity<TId> entity && Id.Equals(entity.Id);

        public bool Equals(Entity<TId>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
            => Id.GetHashCode();

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
            => Equals(left, right);

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
            => !Equals(left, right);

    }
    







    
}
