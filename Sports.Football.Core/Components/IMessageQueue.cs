using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sports.Football.Core.Components
{
    public interface IMessageQueue<TMessage>
    {
        Task EnqueueAsync(TMessage message);
        Task DequeueAsync();
        Task<IEnumerable<TMessage>> DequeueBatchAsync(int batchSize);
    }
}