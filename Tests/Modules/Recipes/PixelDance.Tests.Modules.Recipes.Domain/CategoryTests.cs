using System;
using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Tests.Modules.Recipes.Domain
{
    public class CategoryTests
    {
        [Fact]
        public void Create_Category_Success()
        {
            // Arrange
            var category = new Category("test-category");

            // Act

            // Assert
            category.Should()
                .NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void Create_Category_With_Invalid_Data_Throws_Exception(Type exceptionType, string categoryValue)
        {
            // Arange

            // Act
            Action sutAction = () =>
            {
                _ =  new Category(categoryValue);
            };

            // Assert
            Assert.Throws(exceptionType, sutAction);

            //result
            //    .Should()
            //    .BeOfType(EXPECTED)
            //    .Of(typeof(EXPECTED))
            //    .WithMessage("FirstName is missing");
        }

        [Fact]
        public void Update_Category_Succeed()
        {
            // Arange
            var category = new Category("test-category");
            var expectedCategory = new Category("updated-category");

            // Act
            category.Update(expectedCategory.Name);

            // Assert
            category.Name.Should()
                .Be(expectedCategory.Name);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void Update_Category_With_Invalid_Data_Throws_Exception(Type exceptionType, string categoryValue)
        {
            // Arange
            var sut = new Category("test-category");

            // Act
            Action sutAction = () =>
            {
                sut.Update(categoryValue);
            };

            // Assert
            Assert.Throws(exceptionType, sutAction);

            //result
            //    .Should()
            //    .BeOfType(EXPECTED)
            //    .Of(typeof(EXPECTED))
            //    .WithMessage("FirstName is missing");
        }

        public static IEnumerable<object[]> GetInvalidTestData()
        {
#pragma warning disable CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
            yield return new object[] { typeof(InvalidOperationException), null };
            yield return new object[] { typeof(InvalidOperationException), string.Empty };
            yield return new object[] { typeof(InvalidOperationException), "  " };
#pragma warning restore CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
        }
    }
}
