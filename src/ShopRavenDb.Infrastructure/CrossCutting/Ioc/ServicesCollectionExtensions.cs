namespace ShopRavenDb.Infrastructure.CrossCutting.Ioc
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddRavenDb(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddSingleton<IDocumentStore>(ctx =>
            {
                var store = new DocumentStore
                {
                    Urls = new string[] {"http://127.0.0.1:8080/"},
                    Database = "Shop"
                };

                store.Initialize();

                return store;
            });

            return servicesCollection;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection servicesCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DtoToModelMappingCustomer());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            servicesCollection.AddSingleton(mapper);

            return servicesCollection;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddScoped<ICustomerService, CustomerService>();
            servicesCollection.TryAddScoped<IDocumentService, DocumentService>();

            return servicesCollection;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddScoped<ICustomerApplication, CustomerApplication>();
            servicesCollection.TryAddScoped<IDocumentApplication, DocumentApplication>();
            
            return servicesCollection;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddSingleton<ICustomerRepository, CustomerRepository>();
            servicesCollection.TryAddSingleton<IDocumentRepository, DocumentRepository>();

            return servicesCollection;
        }
    }
}