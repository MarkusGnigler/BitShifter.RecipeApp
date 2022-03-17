using System;

using Xunit;
using FluentAssertions;

using BitShifter.Shared.ROP;
using BitShifter.Tests.Shared.ROP.Fixtures;

namespace BitShifter.Tests.Shared.ROP
{
    public class ResultExtensionTest
    {

        #region [ Bind ]

        [Fact]
        public void Test_Result_Bind()
        {
            //Arrange
            const int EXPECTED = 666;

            Func<User, Result<int, Exception>> bindAction
                = _ => EXPECTED.Succeeded<int, Exception>();

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

            //Act
            int value = result
                .Bind(bindAction)
                .Match(
                    onSuccess: x => x,
                    onFailure: _ => 0);

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
            const int EXPECTED = 666;

            Func<User, int> mapAction = _ => EXPECTED;

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

            //Act
            int value = result
                .Map(mapAction)
                .Match(
                    onSuccess: x => x,
                    onFailure: _ => 0);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_MapFailure()
        {
            //Arrange
            const string EXPECTED = ResultBuilder.FAILURE_EXCEPTION_MESSAGE;

            Func<Exception, string> mapAction = x => x.Message;

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            //Act
            string value = result
                .MapFailure(mapAction)
                .Match(
                    onSuccess: x => string.Empty,
                    onFailure: x => x);

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
            const int EXPECTED = 1;
            int count = 0;

            void teeAction(User _) => count++;

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

            //Act
            result.Tee(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Tee_With_Failure()
        {
            //Arrange
            const int EXPECTED = 0;
            int count = 0;

            void teeAction(User _) => count++;

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            //Act
            result.Tee(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeFailure_With_Success()
        {
            //Arrange
            const int EXPECTED = 0;
            int count = 0;

            void teeAction(Exception _) => count++;

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

            //Act
            result.TeeFailure(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeFailure_With_Failure()
        {
            //Arrange
            const int EXPECTED = 1;
            int count = 0;

            void teeAction(Exception _) => count++;

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            //Act
            result.TeeFailure(teeAction);

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
            const string EXPECTED = "Changed Either";

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

            Func<Result<User, Exception>, Result<User, Exception>> eitherAction
                = _ => new User(EXPECTED).Succeeded<User, Exception>();

            //Act
            User value = result
                .Either(
                    onSuccess: eitherAction,
                    onFailure: error => error)
                .Match(
                    onSuccess: x => x,
                    onFailure: _ => null);

            //Assert
            value.Name.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Either_With_Failure()
        {
            //Arrange
            const string EXPECTED = "Changed Either";

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            Func<Result<User, Exception>, Result<User, Exception>> eitherAction
                = _ => new Exception(EXPECTED).Failed<User, Exception>();

            //Act
            Exception value = result
                .Either(
                    onSuccess: x => x,
                    onFailure: eitherAction)
                .Match(
                    onSuccess: _ => null,
                    onFailure: x => x);

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
            const int EXPECTED = 42;
            int count = 0;

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

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
            const int EXPECTED = 0;
            int count = 0;

            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

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
            const int EXPECTED = 1;
            int count = 0;

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

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
            const int EXPECTED = 0;
            int count = 0;

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            //Act
            result.Handle(
                x => count += 42,
                error => { });

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Match_With_Success()
        {
            //Arrange
            const string EXPECTED = ResultBuilder.FAILURE_USER_NAME;


            Result<User, Exception> result
                = ResultBuilder.GetSuccess();

            //Act
            var value = result
                .Match(
                    onSuccess: x => x,
                    onFailure: _ => null);

            //Assert
            value.Name.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_Match_With_Error()
        {
            //Arrange
            const string EXPECTED = ResultBuilder.FAILURE_EXCEPTION_MESSAGE;

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            //Act
            var value = result
                .Match(
                    onSuccess: _ => string.Empty,
                    onFailure: x => x.Message);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        #endregion

    }
}
