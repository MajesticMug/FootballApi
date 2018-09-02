using AutoMapper;
using Sports.Football.Core.ServiceClient.Model.DTOs;
using Sports.Football.Core.ServiceClient.Model.DTOs.Base;
using Sports.Football.Data.Model;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Core.ServiceClient.Mappers
{
    public class AutoModelMapper : IModelMapper
    {
        private readonly Mapper _mapper;

        public AutoModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaseDto, BaseModel>()
                    .MapIds();

                cfg.CreateMap<AreaDto, Area>()
                    .MapIds();

                cfg.CreateMap<SeasonDto, Season>()
                    .MapIds();

                cfg.CreateMap<CompetitionDto, Competition>()
                    .MapIds()
                    .ForMember(
                        destination => destination.LeagueCode,
                        opt => opt.MapFrom(source => source.Code));

                cfg.CreateMap<TeamDto, Team>()
                    .MapIds()
                    .ForMember(
                        destination => destination.Players, 
                        opt => opt.MapFrom(source => source.Squad));

                cfg.CreateMap<SquadMemberDto, Player>()
                    .MapIds();
            });

            _mapper = new Mapper(config);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.DefaultContext.Mapper.Map<TSource, TDestination>(source);
        }
    }
}