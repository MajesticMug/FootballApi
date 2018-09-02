using Sports.Football.Data.Model;

namespace Sports.Football.Core.Components
{
    public interface IPlayersToImportQueue : IMessageQueue<Player>
    {
        
    }
}