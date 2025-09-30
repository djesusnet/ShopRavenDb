using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRavenDb.Api.Controllers;
using ShopRavenDb.Application.Dtos;
using ShopRavenDb.Application.Interfaces;
using Xunit;

namespace ShopRavenDb.Api.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerApplication> _customerApplicationMock;
        private readonly CustomerController _customerController;

        public CustomerControllerTests()
        {
            _customerApplicationMock = new Mock<ICustomerApplication>();
            _customerController = new CustomerController(_customerApplicationMock.Object);
        }

        [Fact]
        public void AddCustomer_ShouldReturnOkResult()
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

            // Act
            var result = _customerController.AddCustomer(customerDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().Be("Customer Inserted successfully!");
            _customerApplicationMock.Verify(a => a.AddCustomer(customerDto), Times.Once);
        }

        [Fact]
        public void UpdateCustomer_ShouldReturnOkResult()
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

            // Act
            var result = _customerController.UpdateCustomer(customerDto);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().Be("Customer updated successfully!");
            _customerApplicationMock.Verify(a => a.UpdateCustomer(customerDto), Times.Once);
        }

        [Fact]
        public void GetCustomers_ShouldReturnOkResultWithCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new CustomerDto
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
                },
                new CustomerDto
                {
                    Name = "Jane Smith",
                    Email = "jane.smith@email.com",
                    Cpf = "98765432100",
                    BirthDate = DateTime.Now.AddYears(-25),
                    Address = new AddressDto
                    {
                        Street = "Second Street",
                        Number = 456,
                        City = "Los Angeles",
                        State = "CA",
                        PostalCode = "54321"
                    }
                }
            };

            _customerApplicationMock.Setup(a => a.GetCustomers()).Returns(customers);

            // Act
            var result = _customerController.GetCustomers();

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            
            var returnedCustomers = okResult.Value as IEnumerable<CustomerDto>;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().HaveCount(2);
            returnedCustomers.Should().BeEquivalentTo(customers);
            
            _customerApplicationMock.Verify(a => a.GetCustomers(), Times.Once);
        }

        [Fact]
        public void GetCustomerById_ShouldReturnOkResultWithCustomer()
        {
            // Arrange
            var customerId = "customer-123";
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

            _customerApplicationMock.Setup(a => a.GetCustomerById(customerId)).Returns(customerDto);

            // Act
            var result = _customerController.GetCustomerById(customerId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            
            var returnedCustomer = okResult.Value as CustomerDto;
            returnedCustomer.Should().NotBeNull();
            returnedCustomer.Should().BeEquivalentTo(customerDto);
            
            _customerApplicationMock.Verify(a => a.GetCustomerById(customerId), Times.Once);
        }

        [Fact]
        public void DeleteCustomerById_ShouldReturnOkResult()
        {
            // Arrange
            var customerId = "customer-to-delete-123";

            // Act
            var result = _customerController.DeleteCustomerById(customerId);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().Be("Customer deleted successfully!");
            
            _customerApplicationMock.Verify(a => a.DeleteCustomerById(customerId), Times.Once);
        }

        [Fact]
        public void AddCustomer_WhenApplicationThrowsException_ShouldPropagateException()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                Name = "John Doe",
                Email = "invalid-email",
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

            _customerApplicationMock.Setup(a => a.AddCustomer(customerDto))
                .Throws(new Exception("Invalid email"));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _customerController.AddCustomer(customerDto));
            exception.Message.Should().Be("Invalid email");
            _customerApplicationMock.Verify(a => a.AddCustomer(customerDto), Times.Once);
        }
    }
}