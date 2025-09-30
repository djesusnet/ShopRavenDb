using FluentAssertions;
using ShopRavenDb.Domain.Model;
using Xunit;

namespace ShopRavenDb.Domain.Tests.Model
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_ShouldHaveValidProperties()
        {
            // Arrange & Act
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Parse("1990-01-01"),
                IsActive = true,
                Address = new Address
                {
                    Street = "Main Street",
                    Number = 123,
                    Complement = "Apt 4B",
                    City = "New York",
                    State = "NY",
                    PostalCode = "12345"
                }
            };

            // Assert
            customer.Name.Should().Be("John Doe");
            customer.Email.Should().Be("john.doe@email.com");
            customer.Cpf.Should().Be("12345678900");
            customer.BirthDate.Should().Be(DateTime.Parse("1990-01-01"));
            customer.IsActive.Should().BeTrue();
            customer.Address.Should().NotBeNull();
            customer.Address.Street.Should().Be("Main Street");
            customer.Address.Number.Should().Be(123);
            customer.Address.Complement.Should().Be("Apt 4B");
            customer.Address.City.Should().Be("New York");
            customer.Address.State.Should().Be("NY");
            customer.Address.PostalCode.Should().Be("12345");
        }

        [Fact]
        public void Customer_WithoutAddress_ShouldAllowNullAddress()
        {
            // Arrange & Act
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Parse("1990-01-01"),
                IsActive = false,
                Address = null!
            };

            // Assert
            customer.Address.Should().BeNull();
            customer.IsActive.Should().BeFalse();
        }

        [Theory]
        [InlineData("John Doe")]
        [InlineData("Jane Smith")]
        [InlineData("Carlos Silva")]
        [InlineData("Maria Santos")]
        public void Customer_WithDifferentNames_ShouldSetNameCorrectly(string name)
        {
            // Arrange & Act
            var customer = new Customer
            {
                Name = name,
                Email = "test@email.com",
                Cpf = "12345678900",
                BirthDate = DateTime.Now.AddYears(-25),
                IsActive = true,
                Address = new Address()
            };

            // Assert
            customer.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("12345678900")]
        [InlineData("98765432100")]
        [InlineData("11111111111")]
        public void Customer_WithDifferentCpfs_ShouldSetCpfCorrectly(string cpf)
        {
            // Arrange & Act
            var customer = new Customer
            {
                Name = "Test Name",
                Email = "test@email.com",
                Cpf = cpf,
                BirthDate = DateTime.Now.AddYears(-25),
                IsActive = true,
                Address = new Address()
            };

            // Assert
            customer.Cpf.Should().Be(cpf);
        }
    }
}