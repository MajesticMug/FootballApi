using System.Threading.Tasks;

namespace Sports.Football.Core.ServiceClient
{
    public interface IServiceClient
    {
        Task<TRoot> GetRootAsync<TRoot>(string uri);
    }
}