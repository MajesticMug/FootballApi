using System.Diagnostics;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;

namespace Sports.Football.Core.ServiceClient.Mappers
{
    public interface IModelMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}