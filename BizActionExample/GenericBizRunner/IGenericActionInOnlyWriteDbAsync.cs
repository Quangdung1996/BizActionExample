using System;
using System.Collections.Generic;
using System.Text;

namespace GenericBizRunner
{
    public interface IGenericActionInOnlyWriteDbAsync<in TIn> : IGenericActionInOnlyAsync<TIn>
    {
    }
}
