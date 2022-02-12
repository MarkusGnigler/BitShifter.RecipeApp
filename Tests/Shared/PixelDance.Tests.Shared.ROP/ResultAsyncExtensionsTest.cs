using System;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.ROP;

namespace PixelDance.Tests.Shared.ROP
{
    public class ResultAsyncExtensionsTest
    {
        record User(string Name);

        #region [ Bind ]
        [Fact]
        public async Task Test_Result_BindAsync()
        {
            //Arrange
            int EXPECTED = 666;

            Func<User, Task<Result<int, Exception>>> bindAction
                = _ => Task.FromResult(Result<int, Exception>
                    .Succeeded(666));

            Task<Result<User, Exception>> result
                = Task.FromResult(Result<User, Exception>
                    .Succeeded(new(string.Empty)));

            //Act
            int value = (await result
                .BindAsync(bindAction))
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
            var EXPECTED = 666;

            Func<User, Task<int>> mapAction
                = _ => Task.FromResult(666);

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
        public async Task Test_Result_MapAsync()
        {
            //Arrange
            var EXPECTED = 666;

            Func<User, Task<int>> mapAction
                = _ => Task.FromResult(666);

            Task<Result<User, Exception>> result
                = Task.FromResult(Result<User, Exception>
                    .Succeeded(new(string.Empty)));

            //Act
            int value = (await result
                .MapAsync(mapAction))
                .AsSuccess;

            //Assert
            value.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_MapFailure()
        {
            //Arrange
            var EXPECTED = "Test MapFailure";

            Func<Exception, string> mapAction
                = x => x.Message;

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

        //[Fact]
        //public async Task Test_Result_MapFailureAsync()
        //{
        //    //Arrange
        //    var EXPECTED = "Test MapFailure";

        //    Func<Exception, Task<string>> mapAction
        //        = x => Task.FromResult(x.Message);

        //    Task<Result<User, Exception>> result
        //        = Task.FromResult(Result<User, Exception>
        //            .Failed(new Exception("Test MapFailure")));

        //    //Act
        //    string value = (await result
        //        .MapFailureAsync(mapAction))
        //        .AsFailure;

        //    //Assert
        //    value.Should()
        //        .Be(EXPECTED);
        //}
        #endregion

        #region [ Tee ]
        [Fact]
        public void Test_Result_TeeAsync_With_Success()
        {
            //Arrange
            var EXPECTED = 1;
            var count = 0;

            void teeAction(User x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(Result<User, Exception>
                    .Succeeded(new("Test Tee")));

            //Act
            _ = result
                .TeeAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeAsync_With_Failure()
        {
            //Arrange
            var EXPECTED = 0;
            var count = 0;

            void teeAction(User x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(Result<User, Exception>
                    .Failed(new Exception("Test Tee")));

            //Act
            _ = result
                .TeeAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeFailureAsync_With_Success()
        {
            //Arrange
            var EXPECTED = 0;
            var count = 0;

            void teeAction(Exception x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(Result<User, Exception>
                    .Succeeded(new("Test TeeFailure")));

            //Act
            _ = result
                .TeeFailureAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }

        [Fact]
        public void Test_Result_TeeFailureAsync_With_Failure()
        {
            //Arrange
            var EXPECTED = 1;
            var count = 0;

            void teeAction(Exception x) => count++;

            Task<Result<User, Exception>> result
                = Task.FromResult(Result<User, Exception>
                    .Failed(new Exception("Test TeeFailure")));

            //Act
            _ = result
                .TeeFailureAsync(teeAction);

            //Assert
            count.Should()
                .Be(EXPECTED);
        }
        #endregion

    }
}
