namespace ShopRavenDb.Application.Mappers;

public class DtoToModelMappingBuild : Profile
{
    public DtoToModelMappingBuild()
    {
        MapppingBuild();
    }

    private void MapppingBuild()
    {
        CreateMap<BuildDto, Build>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(x => x.Number)).ReverseMap()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => x.IsActive)).ReverseMap();
    }
}