using System.Collections.Generic;

namespace Rx.Data {
    public interface IReactiveDictionary<TKey, TValue> : IReadOnlyReactiveDictionary<TKey, TValue>, IDictionary<TKey, TValue>
    {
    }
}
