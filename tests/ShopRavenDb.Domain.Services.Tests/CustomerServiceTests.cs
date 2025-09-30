using FluentAssertions;
using Moq;
using ShopRavenDb.Domain.Core.Interfaces.Repositories;
using ShopRavenDb.Domain.Core.Interfaces.Services;
using ShopRavenDb.Domain.Core.Interfaces.Validators;
using ShopRavenDb.Domain.Model;
using ShopRavenDb.Domain.Services;
using Xunit;

namespace ShopRavenDb.Domain.Services.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IEmailValidator> _emailValidatorMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _emailValidatorMock = new Mock<IEmailValidator>();
            _customerService = new CustomerService(_customerRepositoryMock.Object, _emailValidatorMock.Object);
        }

        [Fact]
        public void AddCustomer_WithValidEmail_ShouldSetIsActiveToTrueAndCallRepository()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "valid@email.com",
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

            _emailValidatorMock.Setup(v => v.IsValid(customer.Email)).Returns(true);

            // Act
            _customerService.AddCustomer(customer);

            // Assert
            customer.IsActive.Should().BeTrue();
            _customerRepositoryMock.Verify(r => r.AddCustomer(customer), Times.Once);
            _emailValidatorMock.Verify(v => v.IsValid(customer.Email), Times.Once);
        }

        [Fact]
        public void AddCustomer_WithInvalidEmail_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "invalid-email",
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

            _emailValidatorMock.Setup(v => v.IsValid(customer.Email)).Returns(false);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _customerService.AddCustomer(customer));
            exception.Message.Should().Be("Invalid email");
            
            _customerRepositoryMock.Verify(r => r.AddCustomer(It.IsAny<Customer>()), Times.Never);
            _emailValidatorMock.Verify(v => v.IsValid(customer.Email), Times.Once);
        }

        [Fact]
        public void GetCustomerById_ShouldReturnCustomerFromRepository()
        {
            // Arrange
            var customerId = "customer-123";
            var expectedCustomer = new Customer
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

            _customerRepositoryMock.Setup(r => r.GetCustomerById(customerId)).Returns(expectedCustomer);

            // Act
            var result = _customerService.GetCustomerById(customerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedCustomer);
            _customerRepositoryMock.Verify(r => r.GetCustomerById(customerId), Times.Once);
        }

        [Fact]
        public void GetCustomers_ShouldReturnAllCustomersFromRepository()
        {
            // Arrange
            var expectedCustomers = new List<Customer>
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

            _customerRepositoryMock.Setup(r => r.GetCustomers()).Returns(expectedCustomers);

            // Act
            var result = _customerService.GetCustomers();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedCustomers);
            _customerRepositoryMock.Verify(r => r.GetCustomers(), Times.Once);
        }

        [Fact]
        public void UpdateCustomer_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe Updated",
                Email = "john.updated@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Now.AddYears(-30),
                Address = new Address
                {
                    Street = "Updated Street",
                    Number = 789,
                    City = "Updated City",
                    State = "UC",
                    PostalCode = "98765"
                }
            };

            // Act
            _customerService.UpdateCustomer(customer);

            // Assert
            _customerRepositoryMock.Verify(r => r.UpdateCustomer(customer), Times.Once);
        }

        [Fact]
        public void DeleteCustomerById_ShouldCallRepositoryDelete()
        {
            // Arrange
            var customerId = "customer-to-delete-123";

            // Act
            _customerService.DeleteCustomerById(customerId);

            // Assert
            _customerRepositoryMock.Verify(r => r.DeleteCustomerById(customerId), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("invalid")]
        [InlineData("invalid@")]
        [InlineData("@invalid.com")]
        public void AddCustomer_WithVariousInvalidEmails_ShouldThrowException(string invalidEmail)
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Email = invalidEmail,
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

            _emailValidatorMock.Setup(v => v.IsValid(invalidEmail)).Returns(false);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _customerService.AddCustomer(customer));
            exception.Message.Should().Be("Invalid email");
        }
    }
}