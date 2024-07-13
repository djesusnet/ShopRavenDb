using ShopRavenDb.Domain.Core.Interfaces.Validators;

namespace ShopRavenDb.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailValidator _emailValidator;

        public CustomerService(ICustomerRepository customerRepository,
                               IEmailValidator emailValidator)
        {
            _customerRepository = customerRepository;
            _emailValidator = emailValidator;

        }

        public void AddCustomer(Customer customer)
        {
            if (!_emailValidator.IsValid(customer.Email))
            {
                throw new Exception("Invalid email");
            }
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