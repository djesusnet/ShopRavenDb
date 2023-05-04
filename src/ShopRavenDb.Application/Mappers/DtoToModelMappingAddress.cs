namespace ShopRavenDb.Application.Mappers
{
    public class DtoToModelMappingAddress : Profile
    {
        public DtoToModelMappingAddress()
        {
            MappingAddress();
        }

        private void MappingAddress()
        {
            CreateMap<AddressDto, Address>()
                 .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap()
                 .ForMember(dest => dest.Street, opt => opt.MapFrom(x => x.Street)).ReverseMap()
                 .ForMember(dest => dest.Number, opt => opt.MapFrom(x => x.Number)).ReverseMap()
                 .ForMember(dest => dest.Complement, opt => opt.MapFrom(x => x.Complement)).ReverseMap()
                 .ForMember(dest => dest.City, opt => opt.MapFrom(x => x.City)).ReverseMap()
                 .ForMember(dest => dest.State, opt => opt.MapFrom(x => x.State)).ReverseMap()
                 .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(x => x.PostalCode)).ReverseMap();
        }
    }
}