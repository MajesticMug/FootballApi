using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sports.Football.Core.Components
{
    public class AzureStorageMessageQueue<TMessage> : IMessageQueue<TMessage>
    {
        public Task EnqueueAsync(TMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task DequeueAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TMessage>> DequeueBatchAsync(int batchSize)
        {
            throw new System.NotImplementedException();
        }
    }
}