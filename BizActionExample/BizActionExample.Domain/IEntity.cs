using System;
using System.Collections.Generic;
using System.Text;

namespace BizActionExample.Domain
{
    public interface IEntity : IDomainObject
    {
 
        void Init();
    }


    public interface IEntity<out TKey> : IKey<TKey>, IEntity
    {
    }

    public interface IEntity<in TEntity, out TKey> : ICompareChange<TEntity>, IEntity<TKey> where TEntity : IEntity
    {
    }
}
