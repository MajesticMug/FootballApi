using AutoMapper;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Core.ServiceClient.Mappers
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TDto, TModel> MapIds<TDto, TModel>(
            this IMappingExpression<TDto, TModel> expr)
            where TDto : BaseDto
            where TModel : BaseModel
        {
            expr
                .ForMember(
                    destination => destination.ExternalId,
                    opt => opt.MapFrom(source => source.Id))
                .ForMember(
                    destination => destination.Id,
                    opt => opt.Ignore());

            return expr;
        }
    }
}