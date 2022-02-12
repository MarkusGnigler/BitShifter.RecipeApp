using System;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.ROP;

namespace PixelDance.Tests.Shared.ROP
{
    public class ResultTest
    {
        record User(string Name);

        #region [ Creation ]
        [Fact]
        public void Create_Success_Result()
        {
            //Arrange
            var EXPECTED = typeof(Result<User, string[]>.Success);

            Result<User, string[]> result
                = Result<User, string[]>
                    .Succeeded(new(string.Empty));

            //Act

            //Assert
            result.Should()
                .BeOfType(EXPECTED);
        }

        [Fact]
        public void Create_Success_Throws_NullException()
        {
            //Arrange
            string EXPECTED = "Value cannot be null. (Parameter 'success')";

            Func<Result<User, string[]>> createResultAction =
                () => Result<User, string[]>
                    .Succeeded(null!);

            //Act

            //Assert
            createResultAction
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage(EXPECTED);
        }

        [Fact]
        public void Create_Failure_Result()
        {
            //Arrange
            var EXPECTED = typeof(Result<User, string[]>.Failure);

            Result<User, string[]> result
                = Result<User, string[]>
                    .Failed(new[] { string.Empty });

            //Act

            //Assert
            result.Should()
                .BeOfType(EXPECTED);
        }

        [Fact]
        public void Create_Failure_Throws_NullException()
        {
            //Arrange
            string EXPECTED = "Value cannot be null. (Parameter 'failure')";

#pragma warning disable CS8625
            Func<Result<User, string[]>> createResultAction =
                () => Result<User, string[]>
                    .Failed(null);
#pragma warning restore CS8625

            //Act

            //Assert
            createResultAction
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage(EXPECTED);
        }
        #endregion

        #region [ Casting / As operator ]
        [Fact]
        public void Cast_Success_Result_ToRight_Type()
        {
            //Arrange
            var EXPECTED = typeof(Result<User, string[]>.Success);

            Result<User, string[]> result
                = Result<User, string[]>
                    .Succeeded(new(string.Empty));

            //Act
            var value = result.AsSuccess;

            //Assert
            value.Should()
                .BeOfType(EXPECTED);
        }

        [Fact]
        public void Cast_Failure_Result_ToRight_Type()
        {
            //Arrange
            var EXPECTED = typeof(Result<User, string[]>.Failure);

            Result<User, string[]> result
                = Result<User, string[]>
                    .Failed(new[] { string.Empty });

            //Act
            var value = result.AsFailure;

            //Assert
            value.Should()
                .BeOfType(EXPECTED);
        }
        #endregion

        #region [ Querying / Is operator ]
        [Fact]
        public void Test_If_Result_IsSuccess_Is_True()
        {
            //Arrange
            var EXPECTED = true;

            Result<User, string[]> result
                = Result<User, string[]>
                    .Succeeded(new(string.Empty));

            //Act
            var value = result.IsSuccess;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_If_Result_IsSuccess_Is_False()
        {
            //Arrange
            var EXPECTED = false;

            Result<User, string[]> result
                = Result<User, string[]>
                    .Failed(new[] { string.Empty });

            //Act
            var value = result.IsSuccess;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_If_Result_IsFailure_Is_True()
        {
            //Arrange
            var EXPECTED = true;

            Result<User, string[]> result
                = Result<User, string[]>
                    .Failed(new[] { string.Empty });

            //Act
            var value = result.IsFailure;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_If_Result_IsFailure_Is_False()
        {
            //Arrange
            var EXPECTED = false;

            Result<User, string[]> result
                = Result<User, string[]>
                    .Succeeded(new(string.Empty));

            //Act
            var value = result.IsFailure;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }
        #endregion

    }
}
