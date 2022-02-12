using System;
using System.Linq;
using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Tests.Modules.Recipes.Domain
{
    public class RecipeTests
    {
        [Fact]
        public void Create_Recipe_Succeed()
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);

            // Act

            // Assert
            recipe.Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void Create_Recipe_With_Invalid_Data_Throws_Exception(Type exceptionType, string[] recipeValues)
        {
            // Arange

            // Act
            Action sutAction = () =>
            {
                _ = new Recipe(
                    recipeValues[0], recipeValues[1], recipeValues[2],
                    recipeValues[3], recipeValues[4], new Category(recipeValues[5]));
            };

            // Assert
            Assert.False(recipeValues.Length != 6, $"{nameof(GetInvalidTestData)} must contain a string array with lentgh of 6");
            Assert.Throws(exceptionType, sutAction);

            //result
            //    .Should()
            //    .BeOfType(EXPECTED)
            //    .Of(typeof(EXPECTED))
            //    .WithMessage("FirstName is missing");
        }

        [Fact]
        public void Update_Recipe_Succeed()
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);
            var expectedCategory = new Category("updated-category");
            var expectedRecipeData = new Recipe(
                "update-recipe", "Update Recipe", "update-recipe-image.png",
                "update perparation", "update description", category);

            // Act
            recipe.Update(
                expectedRecipeData.Slug, expectedRecipeData.Title, expectedRecipeData.Img,
                expectedRecipeData.Preparation, expectedRecipeData.Description, true, 42, expectedCategory);

            // Assert
            recipe.Slug.Should()
                .Be(expectedRecipeData.Slug);
            recipe.Title.Should()
                .Be(expectedRecipeData.Title);
            recipe.Img.Should()
                .Be(expectedRecipeData.Img);
            recipe.Preparation.Should()
                .Be(expectedRecipeData.Preparation);
            recipe.Description.Should()
                .Be(expectedRecipeData.Description);
            recipe.Liked.Should()
                .BeTrue();
            recipe.Position.Should()
                .Be(42);
            recipe.Category.Name.Should()
                .Be(expectedCategory.Name);
        }

        [Fact]
        public void Update_Recipe_Position_Failed()
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);

            // Act
            Action sutAction = () =>
            {
                recipe.Update(
                    "test-recipe", "Test Recipe", "test-recipe-image.png",
                    "test perparation", "test description", false, 0, category);
            };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(sutAction);

            //result
            //    .Should()
            //    .BeOfType(EXPECTED)
            //    .Of(typeof(EXPECTED))
            //    .WithMessage("FirstName is missing");
        }

        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void Update_Recipe_With_Invalid_Data_Throws_Exception(Type exceptionType, string[] recipeValues)
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);

            // Act
            Action sutAction = () =>
            {
                recipe.Update(
                    recipeValues[0], recipeValues[1], recipeValues[2],
                    recipeValues[3], recipeValues[4], false, 1, new Category(recipeValues[5]));
            };

            // Assert
            Assert.False(recipeValues.Length != 6, $"{nameof(GetInvalidTestData)} must contain a string array with lentgh of 6");
            Assert.Throws(exceptionType, sutAction);

            //result
            //    .Should()
            //    .BeOfType(EXPECTED)
            //    .Of(typeof(EXPECTED))
            //    .WithMessage("FirstName is missing");
        }

        [Fact]
        public void Insert_Ingredient_With_Parameters_Into_Recipe_Succeed()
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);
            var ingredient = Ingredient.Create("Ingredient Title", 1.5, "Create Unit");

            // Act
            recipe.InsertIngredient("Ingredient Title", 1.5, "Create Unit");

            // Assert
            recipe.Ingredients
                .FirstOrDefault(x => x.Equals(ingredient))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void Insert_Ingredient_With_Object_Into_Recipe_Succeed()
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);
            var ingredient = Ingredient.Create("Ingredient Title", 1.5, "Create Unit");

            // Act
            recipe.InsertIngredient(ingredient);

            // Assert
            recipe.Ingredients
                .FirstOrDefault(x => x.Equals(ingredient))
                .Should()
                .NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GetInvalidIngredientData))]
        public void Update_Ingredient_Succeed(Ingredient expected, Ingredient forUpdate)
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);
            var ingredient = Ingredient.Create("null", 1, "null");

            // Act
            recipe.InsertIngredient(ingredient.Title, ingredient.Quantity, ingredient.Unit);

            var newIngredients = recipe.Ingredients
                .Select(x => !x.Equals(forUpdate) 
                    ? x 
                    : Ingredient.Create(ingredient.Title, ingredient.Quantity, ingredient.Unit));

            recipe.UpdateIngredients(ingredient, forUpdate);

            // Assert
            recipe.Ingredients
                .FirstOrDefault(x => x.Equals(expected))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void Update_Ingredient_With_List_Succeed()
        {
            // Arange
            var ingredients = GetInvalidIngredientData()
                .SelectMany(x => x.Select(y => y as Ingredient));

            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);
            var forUpdate = ingredients.First();
            var expected = Ingredient.Create("null", 1, "null");

            // Act
            recipe.UpdateIngredients(ingredients);

            var newIngredients = recipe.Ingredients
                .Select(x => !x.Equals(forUpdate)
                    ? x
                    : Ingredient.Create(expected.Title, expected.Quantity, expected.Unit));

            recipe.UpdateIngredients(newIngredients);

            // Assert
            recipe.Ingredients
                .FirstOrDefault(x => x.Equals(expected))
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void Remove_Ingredient_Into_Recipe_Succeed()
        {
            // Arange
            var category = new Category("test-category");
            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);
            var ingredient = Ingredient.Create("Ingredient Title", 1.5, "Create Unit");

            // Act
            recipe.InsertIngredient(ingredient.Title, ingredient.Quantity, ingredient.Unit);
            recipe.RemoveIngredient(ingredient.Title, ingredient.Quantity, ingredient.Unit);

            // Assert
            recipe.Ingredients
                .FirstOrDefault(x => x.Equals(ingredient))
                .Should()
                .BeNull();
        }

        public static IEnumerable<object[]> GetInvalidTestData()
        {
#pragma warning disable CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { null, "Name", "Image", "Preparation", "Description", "CategoryName" } };
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { "Slug", null, "Image", "Preparation", "Description", "CategoryName" } };
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { "Slug", "Name", "Image", null, "Description", "CategoryName" } };
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { "Slug", "Name", "Image", "Preparation", null, "CategoryName" } };

            yield return new object[] { typeof(InvalidOperationException),
                new string[] { string.Empty, "Name", "Image", "Preparation", "Description", "CategoryName" } };
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { "Slug", string.Empty, "Image", "Preparation", "Description", "CategoryName" } };
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { "Slug", "Name", "Image", string.Empty, "Description", "CategoryName" } };
            yield return new object[] { typeof(InvalidOperationException),
                new string[] { "Slug", "Name", "Image", "Preparation", string.Empty, "CategoryName" } };
#pragma warning restore CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
        }

        public static IEnumerable<object[]> GetInvalidIngredientData()
        {
            yield return new object[] { Ingredient.Create("Test Title", 1, "Unit"), Ingredient.Create("Test Title", 1, "Unit") };
            yield return new object[] { Ingredient.Create("Title", 55, "Unit"), Ingredient.Create("Title", 55, "Unit") };
            yield return new object[] { Ingredient.Create("Title", 1, "Test Unit"), Ingredient.Create("Title", 1, "Test Unit") };
        }
    }
}

