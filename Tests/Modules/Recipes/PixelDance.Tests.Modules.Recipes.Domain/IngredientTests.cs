using System;
using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Tests.Modules.Recipes.Domain
{
    public class IngredientTests
    {
        [Fact]
        public void Is_Ingredient_ValueObject()
        {
            // Arrange
            var ingredient1 = Ingredient.Create("Title", 1.5, "Unit");
            var ingredient2 = Ingredient.Create("Title", 1.5, "Unit");

            // Act

            // Assert
            ingredient1.Should()
                .BeEquivalentTo(ingredient2);
        }

        [Fact]
        public void Create_Ingredient_Success()
        {
            // Arrange
            var ingredient = Ingredient.Create("Title", 1.5, "Unit");

            // Act

            // Assert
            ingredient.Should()
                .NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void Create_Ingredient_Failed(Type exceptionType, string[] ingredientValues)
        {
            // Arrange

            // Act
            Action sutAction = () => {
                Ingredient.Create(ingredientValues[0], double.Parse(ingredientValues[1]), ingredientValues[2]);
            };

            // Assert
            Assert.False(ingredientValues.Length != 3, $"{nameof(GetInvalidTestData)} must contain a string array with lentgh of 3");
            //ingredientValues.Should().HaveCount(3);

            //result
            //    .Should()
            //    .BeOfType(EXPECTED)
            //    .Of(typeof(EXPECTED))
            //    .WithMessage("FirstName is missing");

            Assert.Throws(exceptionType, sutAction);
        }

        public static IEnumerable<object[]> GetInvalidTestData()
        {
#pragma warning disable CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
            yield return new object[] { typeof(ArgumentNullException),
                new string[] { null, "15", "Unit" } };
            yield return new object[] { typeof(ArgumentNullException),
                new string[] { "Title", "15", null } };

            yield return new object[] { typeof(ArgumentNullException),
                new string[] { string.Empty, "15", "Unit" } };
            yield return new object[] { typeof(ArgumentNullException),
                new string[] { "Title", "15", string.Empty } };

            yield return new object[] { typeof(ArgumentOutOfRangeException),
                new string[] { "Title", "0", null } };
#pragma warning restore CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
        }
    }
}
