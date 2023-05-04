namespace ShopRavenDb.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(Customer customer)
        {
            customer.Address.IsActive = true;
            customer.IsActive = true;
            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomerById(string id)
        {
            _customerRepository.DeleteCustomerById(id);
        }

        public Customer GetCustomerById(string id)
        {
            return _customerRepository.GetCustomerById(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }
    }
}