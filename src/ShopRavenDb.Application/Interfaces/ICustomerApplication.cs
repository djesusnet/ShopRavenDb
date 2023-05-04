namespace ShopRavenDb.Application.Interfaces
{
    public interface ICustomerApplication
    {
        void AddCustomer(CustomerDto customerDto);

        void UpdateCustomer(CustomerDto customerDto);

        void DeleteCustomerById(string id);

        IEnumerable<CustomerDto> GetCustomers();

        CustomerDto GetCustomerById(string id);
    }
}