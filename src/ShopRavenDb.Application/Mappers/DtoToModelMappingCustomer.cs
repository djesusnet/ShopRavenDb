namespace ShopRavenDb.Application.Mappers
{
    public class DtoToModelMappingCustomer : Profile
    {
        public DtoToModelMappingCustomer()
        {
            MapppingCustomer();
        }

        private void MapppingCustomer()
        {
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name)).ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email)).ReverseMap()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(x => x.BirthDate)).ReverseMap()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(x => x.Address)).ReverseMap()
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(x => x.Cpf)).ReverseMap()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()).ReverseMap();
        }
    }
}