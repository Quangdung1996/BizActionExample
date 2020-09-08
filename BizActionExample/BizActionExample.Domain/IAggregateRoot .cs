using System;

namespace BizActionExample.Domain
{
    public interface IAggregateRoot : IEntity, IVersion
    {
    }
    public interface IAggregateRoot<out TKey> : IEntity<TKey>, IAggregateRoot
    {
    }

    public interface IAggregateRoot<in TEntity, out TKey> : IEntity<TEntity, TKey>, IAggregateRoot<TKey> where TEntity : IAggregateRoot
    {
    }
}
