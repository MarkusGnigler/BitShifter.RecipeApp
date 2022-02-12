using System;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.ROP;

namespace PixelDance.Tests.Shared.ROP
{
    public class ResultExtensionTest
    {
        record User(string Name);

        #region [ Bind ]
        [Fact]
        public void Test_Result_Bind()
        {
            //Arrange
            int EXPECTED = 666;

            Func<User, Result<int, Exception>> bindAction
                = _ => Result<int, Exception>
                    .Succeeded(666);

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new(string.Empty));

            //Act
            int value = result
                .Bind(bindAction)
                .AsSuccess;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }
        #endregion

        #region [ Map ]
        [Fact]
        public void Test_Result_Map()
        {
            //Arrange
            int EXPECTED = 666;

            Func<User, int> mapAction = _ => 666;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new(string.Empty));

            //Act
            int value = result
                .Map(mapAction)
                .AsSuccess;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_MapFailure()
        {
            //Arrange
            string EXPECTED = "Test MapFailure";

            Func<Exception, string> mapAction = x => x.Message;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new Exception("Test MapFailure"));

            //Act
            string value = result
                .MapFailure(mapAction)
                .AsFailure;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }
        #endregion

        #region [ Tee ]
        [Fact]
        public void Test_Result_Tee_With_Success()
        {
            //Arrange
            int EXPECTED = 1;
            int count = 0;

            void teeAction(User x) => count++;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new("Test Tee"));

            //Act
            _ = result
                .Tee(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Tee_With_Failure()
        {
            //Arrange
            int EXPECTED = 0;
            int count = 0;

            void teeAction(User x) => count++;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new Exception("Test Tee"));

            //Act
            _ = result
                .Tee(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeFailure_With_Success()
        {
            //Arrange
            int EXPECTED = 0;
            int count = 0;

            void teeAction(Exception x) => count++;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new("Test TeeFailure"));

            //Act
            _ = result
                .TeeFailure(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeFailure_With_Failure()
        {
            //Arrange
            int EXPECTED = 1;
            int count = 0;

            void teeAction(Exception x) => count++;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new Exception("Test TeeFailure"));

            //Act
            _ = result
                .TeeFailure(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }
        #endregion

        #region [ Either ]
        [Fact]
        public void Test_Result_Either_With_Success()
        {
            //Arrange
            var EXPECTED = new User("Changed Either");

            Func<Result<User, Exception>, Result<User, Exception>> eitherAction
                = x => Result<User, Exception>
                    .Succeeded(new("Changed Either"));

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new("Test Either"));

            //Act
            User value = result
                .Either(
                    onSuccess: eitherAction,
                    onFailure: error => error)
                .AsSuccess;

            //Assert
            value.Should()
                .Be(EXPECTED);

        }

        [Fact]
        public void Test_Result_Either_With_Failure()
        {
            //Arrange
            string EXPECTED = "Changed Either";

            Func<Result<User, Exception>, Result<User, Exception>> eitherAction
                = x => Result<User, Exception>
                    .Failed(new Exception("Changed Either"));

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new Exception("Test Either"));

            //Act
            Exception value = result
                .Either(
                    onSuccess: x => x,
                    onFailure: eitherAction)
                .AsFailure;

            //Assert
            value.Message.Should()
                .Be(EXPECTED);
        }
        #endregion

        #region [ Dead End ]
        [Fact]
        public void Test_Result_Handle_With_Failure()
        {
            //Arrange
            int EXPECTED = 42;
            int count = 0;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new("Test Handle"));

            //Act
            result.Handle(
                x => count += 42,
                error => { });

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Handle_Not_With_Failure()
        {
            //Arrange
            int EXPECTED = 0;
            int count = 0;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new("Test Handle"));

            //Act
            result.Handle(
                x => { },
                error => count += 42);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Handle_With_Error()
        {
            //Arrange
            int EXPECTED = 1;
            int count = 0;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new Exception("Test Handle"));

            //Act
            result.Handle(
                x => { },
                error => count++);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Handle_Not_With_Error()
        {
            //Arrange
            int EXPECTED = 0;
            int count = 0;

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new Exception("Test Handle"));

            //Act
            result.Handle(
                x => count += 42,
                error => { });

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Merge_With_Success()
        {
            //Arrange
            string EXPECTED = "Merge";

            Result<User, Exception> result
                = Result<User, Exception>
                    .Succeeded(new("Merge"));

            //Act
            string value = result
                .Map(x => x.Name)
                .Merge(
                    x => x,
                    error => error.Message);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Merge_With_Error()
        {
            //Arrange
            string EXPECTED = "Merge";

            Result<User, Exception> result
                = Result<User, Exception>
                    .Failed(new("Merge"));

            //Act
            string value = result
                .Map(x => x.Name)
                .Merge(
                    x => x,
                    error => error.Message);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }
        #endregion

    }
}
