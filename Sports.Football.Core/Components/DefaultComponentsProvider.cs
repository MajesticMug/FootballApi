namespace Sports.Football.Core.Components
{
    public class DefaultComponentsProvider : IComponentsProvider
    {
        public DefaultComponentsProvider(
            IFootballManager footballManager, 
            ILogManager logManager)
        {
            FootballManager = footballManager;
            LogManager = logManager;
        }

        public IFootballManager FootballManager { get; }
        public ILogManager LogManager { get; }
    }
}