namespace ShopRavenDb.Application.Mappers
{
    public class DtoToModelMappingCustomer : Profile
    {
        public DtoToModelMappingCustomer()
        {
            MapppingCustomer();
            MapppingAddress();
        }

        private void MapppingCustomer()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name)).ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email)).ReverseMap()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(x => x.BirthDate)).ReverseMap()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(x => x.Address)).ReverseMap()
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(x => x.Cpf)).ReverseMap()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();
        }

        private void MapppingAddress()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(x => x.Street)).ReverseMap()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(x => x.Number)).ReverseMap()
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(x => x.Complement)).ReverseMap()
                .ForMember(dest => dest.City, opt => opt.MapFrom(x => x.City)).ReverseMap()
                .ForMember(dest => dest.State, opt => opt.MapFrom(x => x.State)).ReverseMap()
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(x => x.PostalCode)).ReverseMap()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();
        }
    }
}