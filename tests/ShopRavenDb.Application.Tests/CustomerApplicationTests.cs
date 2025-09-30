using AutoMapper;
using FluentAssertions;
using Moq;
using ShopRavenDb.Application.Dtos;
using ShopRavenDb.Domain.Core.Interfaces.Services;
using ShopRavenDb.Domain.Model;
using Xunit;

namespace ShopRavenDb.Application.Tests
{
    public class CustomerApplicationTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CustomerApplication _customerApplication;

        public CustomerApplicationTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _mapperMock = new Mock<IMapper>();
            _customerApplication = new CustomerApplication(_customerServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void AddCustomer_ShouldCallServiceWithMappedCustomer()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Now.AddYears(-30),
                Address = new AddressDto
                {
                    Street = "Main Street",
                    Number = 123,
                    City = "New York",
                    State = "NY",
                    PostalCode = "12345"
                }
            };

            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Cpf = customerDto.Cpf,
                BirthDate = customerDto.BirthDate,
                Address = new Address
                {
                    Street = customerDto.Address.Street,
                    Number = customerDto.Address.Number,
                    City = customerDto.Address.City,
                    State = customerDto.Address.State,
                    PostalCode = customerDto.Address.PostalCode
                }
            };

            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customer);

            // Act
            _customerApplication.AddCustomer(customerDto);

            // Assert
            _customerServiceMock.Verify(s => s.AddCustomer(customer), Times.Once);
            _mapperMock.Verify(m => m.Map<Customer>(customerDto), Times.Once);
        }

        [Fact]
        public void GetCustomerById_ShouldReturnMappedCustomerDto()
        {
            // Arrange
            var customerId = "customer-123";
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Now.AddYears(-30),
                Address = new Address
                {
                    Street = "Main Street",
                    Number = 123,
                    City = "New York",
                    State = "NY",
                    PostalCode = "12345"
                }
            };

            var customerDto = new CustomerDto
            {
                Name = customer.Name,
                Email = customer.Email,
                Cpf = customer.Cpf,
                BirthDate = customer.BirthDate,
                Address = new AddressDto
                {
                    Street = customer.Address.Street,
                    Number = customer.Address.Number,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    PostalCode = customer.Address.PostalCode
                }
            };

            _customerServiceMock.Setup(s => s.GetCustomerById(customerId)).Returns(customer);
            _mapperMock.Setup(m => m.Map<CustomerDto>(customer)).Returns(customerDto);

            // Act
            var result = _customerApplication.GetCustomerById(customerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(customerDto);
            _customerServiceMock.Verify(s => s.GetCustomerById(customerId), Times.Once);
            _mapperMock.Verify(m => m.Map<CustomerDto>(customer), Times.Once);
        }

        [Fact]
        public void GetCustomers_ShouldReturnMappedCustomerDtos()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer
                {
                    Name = "John Doe",
                    Email = "john.doe@email.com",
                    Cpf = "12345678900",
                    BirthDate = DateTime.Now.AddYears(-30),
                    Address = new Address { Street = "Main Street", Number = 123, City = "New York", State = "NY", PostalCode = "12345" }
                },
                new Customer
                {
                    Name = "Jane Smith",
                    Email = "jane.smith@email.com",
                    Cpf = "98765432100",
                    BirthDate = DateTime.Now.AddYears(-25),
                    Address = new Address { Street = "Second Street", Number = 456, City = "Los Angeles", State = "CA", PostalCode = "54321" }
                }
            };

            var customerDtos = customers.Select(c => new CustomerDto
            {
                Name = c.Name,
                Email = c.Email,
                Cpf = c.Cpf,
                BirthDate = c.BirthDate,
                Address = new AddressDto
                {
                    Street = c.Address.Street,
                    Number = c.Address.Number,
                    City = c.Address.City,
                    State = c.Address.State,
                    PostalCode = c.Address.PostalCode
                }
            }).ToList();

            _customerServiceMock.Setup(s => s.GetCustomers()).Returns(customers);
            _mapperMock.Setup(m => m.Map<IEnumerable<CustomerDto>>(customers)).Returns(customerDtos);

            // Act
            var result = _customerApplication.GetCustomers();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(customerDtos);
            _customerServiceMock.Verify(s => s.GetCustomers(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<CustomerDto>>(customers), Times.Once);
        }

        [Fact]
        public void UpdateCustomer_ShouldCallServiceWithMappedCustomer()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                Name = "John Doe Updated",
                Email = "john.updated@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Now.AddYears(-30),
                Address = new AddressDto
                {
                    Street = "Updated Street",
                    Number = 789,
                    City = "Updated City",
                    State = "UC",
                    PostalCode = "98765"
                }
            };

            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Cpf = customerDto.Cpf,
                BirthDate = customerDto.BirthDate,
                Address = new Address
                {
                    Street = customerDto.Address.Street,
                    Number = customerDto.Address.Number,
                    City = customerDto.Address.City,
                    State = customerDto.Address.State,
                    PostalCode = customerDto.Address.PostalCode
                }
            };

            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customer);

            // Act
            _customerApplication.UpdateCustomer(customerDto);

            // Assert
            _customerServiceMock.Verify(s => s.UpdateCustomer(customer), Times.Once);
            _mapperMock.Verify(m => m.Map<Customer>(customerDto), Times.Once);
        }

        [Fact]
        public void DeleteCustomerById_ShouldCallServiceWithCorrectId()
        {
            // Arrange
            var customerId = "customer-to-delete-123";

            // Act
            _customerApplication.DeleteCustomerById(customerId);

            // Assert
            _customerServiceMock.Verify(s => s.DeleteCustomerById(customerId), Times.Once);
        }
    }
}