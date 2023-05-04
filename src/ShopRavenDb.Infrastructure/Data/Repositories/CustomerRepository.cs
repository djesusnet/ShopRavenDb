using Raven.Client.Documents.Session;

namespace ShopRavenDb.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDocumentStore _documentStore;

        public CustomerRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void AddCustomer(Customer customer)
        {
            using IDocumentSession documentSession = _documentStore.OpenSession();
            documentSession.Store(customer);
            documentSession.SaveChanges();
        }

        public void DeleteCustomerById(string id)
        {
            using IDocumentSession documentSession = _documentStore.OpenSession();
            var customer = documentSession.Load<Customer>(id);
            if (customer is not null)
            {
                documentSession.Delete(customer);
                documentSession.SaveChanges();
            }
        }

        public Customer GetCustomerById(string id)
        {
            using IDocumentSession documentSession = _documentStore.OpenSession();
            var customer = documentSession.Load<Customer>(id);
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            using IDocumentSession documentSession = _documentStore.OpenSession();
            var customers = documentSession.Query<Customer>().ToList();
            return customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            using IDocumentSession documentSession = _documentStore.OpenSession();
            var customerEntity = documentSession.Query<Customer>()
                                                .FirstOrDefault(c => c.Name == customer.Name);

            if (customerEntity is not null)
            {
                customerEntity.Name = customer.Name;
                customerEntity.Email = customer.Email;
                customerEntity.Address = customer.Address;
                customerEntity.Cpf = customer.Cpf;
            }

            documentSession.SaveChanges();
        }
    }
}