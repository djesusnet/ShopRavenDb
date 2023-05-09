namespace ShopRavenDb.Application.Mappers;

public class DtoToModelMappingVersioningFile : Profile
{
    public DtoToModelMappingVersioningFile()
    {
        MapppingVersioningFile();
    }

    private void MapppingVersioningFile()
    {
        CreateMap<VersioningFileDto, VersioningFile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap()
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => x.CreateDate)).ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name)).ReverseMap()
            .ForMember(dest => dest.Version, opt => opt.MapFrom(x => x.Version)).ReverseMap()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description)).ReverseMap()
            .ForMember(dest => dest.Builds, opt => opt.MapFrom(x => x.Builds)).ReverseMap();
    }
}