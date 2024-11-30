using System.ComponentModel.DataAnnotations;

namespace Chronicle.Domain.Primitives;

public abstract class Entity
{
    [Key]
    public int Id { get; protected set; }

    public Guid UserId { get; protected set; }

    //private readonly List<DomainEvent> _domainEvents = new();
    //public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    //protected void AddDomainEvent(DomainEvent domainEvent)
    //{
    //    _domainEvents.Add(domainEvent);
    //}

    //public void RemoveDomainEvent(DomainEvent domainEvent)
    //{
    //    _domainEvents.Remove(domainEvent);
    //}

    //public void ClearDomainEvents()
    //{
    //    _domainEvents.Clear();
    //}

    // Equality checks based on identity
    //public override bool Equals(object obj) =>
    //    obj is Entity entity && Id == entity.Id;

    //public override int GetHashCode() =>
    //    Id.GetHashCode();
}

