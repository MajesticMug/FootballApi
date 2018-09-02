namespace Sports.Football.Core.Components
{
    public interface IComponentsProvider
    {
        IFootballManager FootballManager { get; }
        ILogManager LogManager { get; }
    }
}