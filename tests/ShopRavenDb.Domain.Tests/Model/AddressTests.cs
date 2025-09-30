using FluentAssertions;
using ShopRavenDb.Domain.Model;
using Xunit;

namespace ShopRavenDb.Domain.Tests.Model
{
    public class AddressTests
    {
        [Fact]
        public void Address_ShouldHaveValidProperties()
        {
            // Arrange & Act
            var address = new Address
            {
                Street = "Main Street",
                Number = 123,
                Complement = "Apt 4B",
                City = "New York",
                State = "NY",
                PostalCode = "12345"
            };

            // Assert
            address.Street.Should().Be("Main Street");
            address.Number.Should().Be(123);
            address.Complement.Should().Be("Apt 4B");
            address.City.Should().Be("New York");
            address.State.Should().Be("NY");
            address.PostalCode.Should().Be("12345");
        }

        [Theory]
        [InlineData("Main Street", 123, "New York", "NY", "12345")]
        [InlineData("Second Avenue", 456, "Los Angeles", "CA", "54321")]
        [InlineData("Third Boulevard", 789, "Chicago", "IL", "98765")]
        public void Address_WithDifferentValues_ShouldSetPropertiesCorrectly(
            string street, int number, string city, string state, string postalCode)
        {
            // Arrange & Act
            var address = new Address
            {
                Street = street,
                Number = number,
                City = city,
                State = state,
                PostalCode = postalCode
            };

            // Assert
            address.Street.Should().Be(street);
            address.Number.Should().Be(number);
            address.City.Should().Be(city);
            address.State.Should().Be(state);
            address.PostalCode.Should().Be(postalCode);
        }

        [Fact]
        public void Address_WithoutComplement_ShouldAllowNullComplement()
        {
            // Arrange & Act
            var address = new Address
            {
                Street = "Main Street",
                Number = 123,
                Complement = null!,
                City = "New York",
                State = "NY",
                PostalCode = "12345"
            };

            // Assert
            address.Complement.Should().BeNull();
            address.Street.Should().NotBeNullOrEmpty();
            address.City.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(999)]
        [InlineData(1234)]
        public void Address_WithDifferentNumbers_ShouldSetNumberCorrectly(int number)
        {
            // Arrange & Act
            var address = new Address
            {
                Street = "Test Street",
                Number = number,
                City = "Test City",
                State = "TS",
                PostalCode = "12345"
            };

            // Assert
            address.Number.Should().Be(number);
        }
    }
}