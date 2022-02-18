using System;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.ROP;
using PixelDance.Tests.Shared.ROP.Fixtures;

namespace PixelDance.Tests.Shared.ROP
{
    public class ResultAsyncExtensionsTest
    {

        #region [ Bind ]

        [Fact]
        public async Task Test_Result_BindAsync()
        {
            //Arrange
            const int EXPECTED = 666;

            Func<User, Task<Result<int, Exception>>> bindAction
                = _ => Task.FromResult(
                    EXPECTED.Succeeded<int, Exception>());

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetSuccess());

            //Act
            int value = await result
                .BindAsync(bindAction)
                .MatchAsync(
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

            Func<User, Task<int>> mapAction
                = _ => Task.FromResult(EXPECTED);

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
        public async Task Test_Result_MapAsync_With_Normal_Value()
        {
            //Arrange
            const int EXPECTED = 666;

            Func<User, int> mapAction
                = _ => 666;

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetSuccess());

            //Act
            int value = await result
                .MapAsync(mapAction)
                .MatchAsync(
                    onSuccess: x => x,
                    onFailure: _ => 0);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public async Task Test_Result_MapAsync_With_Async_Value()
        {
            //Arrange
            const int EXPECTED = 666;

            Func<User, Task<int>> mapAction
                = _ => Task.FromResult(666);

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetSuccess());

            //Act
            int value = await result
                .MapAsync(mapAction)
                .MatchAsync(
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

            Func<Exception, Task<string>> mapAction
                = x => Task.FromResult(x.Message);

            Result<User, Exception> result
                = ResultBuilder.GetFailed();

            //Act
            string value = result
                .MapFailure(mapAction)
                .Match(
                    onSuccess: _ => string.Empty,
                    onFailure: x => x);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public async Task Test_Result_MapFailureAsync()
        {
            //Arrange
            const string EXPECTED = ResultBuilder.FAILURE_EXCEPTION_MESSAGE;

            Func<Exception, Task<string>> mapAction
                = x => Task.FromResult(x.Message);

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetFailed());

            //Act
            string value = await result
                .MapFailureAsync(mapAction)
                .MatchAsync(
                    onSuccess: _ => string.Empty,
                    onFailure: x => x);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        #endregion

        #region [ Tee ]

        [Fact]
        public async Task Test_Result_TeeAsync_With_Success()
        {
            //Arrange
            const int EXPECTED = 1;
            int count = 0;

            void teeAction(User x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetSuccess());

            //Act
            await result.TeeAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public async Task Test_Result_TeeAsync_With_Failure()
        {
            //Arrange
            const int EXPECTED = 0;
            int count = 0;

            void teeAction(User x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetFailed());

            //Act
            await result.TeeAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public async Task Test_Result_TeeFailureAsync_With_Success()
        {
            //Arrange
            const int EXPECTED = 0;
            int count = 0;

            void teeAction(Exception x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetSuccess());

            //Act
            await result.TeeFailureAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public async Task Test_Result_TeeFailureAsync_With_Failure()
        {
            //Arrange
            const int EXPECTED = 1;
            int count = 0;

            void teeAction(Exception x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetFailed());

            //Act
            await result.TeeFailureAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        #endregion

        #region [ Match ]

        [Fact]
        public async Task Test_Result_MatchAsync_With_Success()
        {
            //Arrange
            const string EXPECTED = ResultBuilder.FAILURE_USER_NAME;

            Task<Result<User, Exception>> result
                = Task.FromResult(
                    ResultBuilder.GetSuccess());

            //Act
            var value = await result
                .MatchAsync(
                    onSuccess: x => x,
                    onFailure: _ => null);

            //Assert
            value.Name.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public async Task Test_Result_MatchAsync_With_Error()
        {
            //Arrange
            const string EXPECTED = ResultBuilder.FAILURE_EXCEPTION_MESSAGE;

            Task<Result<User, Exception>> result
                = Task.FromResult(ResultBuilder.GetFailed());

            //Act
            var value = await result
                .MatchAsync(
                    onSuccess: _ => string.Empty,
                    onFailure: x => x.Message);

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        #endregion

    }
}
