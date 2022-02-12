using System;
using System.Threading.Tasks;

namespace PixelDance.Shared.ROP
{
    public static class ResultAsyncExtensions
    {
        #region [ Bind ]
        /// <summary>
        /// Takes a Result and creates a new Result.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccessNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="switchFunction"></param>
        /// <returns>Result<TSuccessNew, TFailure></returns>
        public static Result<TSuccessNew, TFailure> Bind<TSuccess, TFailure, TSuccessNew>(
            this Result<TSuccess, TFailure> input,
            Func<TSuccess, Task<Result<TSuccessNew, TFailure>>> switchFunction)
                => input.IsSuccess
                    ? Task.Run(() => switchFunction(input.AsSuccess)).Result.AsSuccess
                    : Result<TSuccessNew, TFailure>.Failed(input.AsFailure);
        /// <summary>
        /// Takes a Result and creates a new Result.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccessNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="switchFunction"></param>
        /// <returns>Result<TSuccessNew, TFailure></returns>
        public static async Task<Result<TSuccessNew, TFailure>> BindAsync<TSuccess, TFailure, TSuccessNew>(
            this Task<Result<TSuccess, TFailure>> input,
            Func<TSuccess, Task<Result<TSuccessNew, TFailure>>> switchFunction)
                => input.Result.IsSuccess
                    ? await switchFunction(input.Result.AsSuccess)
                    : Result<TSuccessNew, TFailure>.Failed(input.Result.AsFailure);
        #endregion

        #region [ Map ]
        /// <summary>
        /// Map the success track to a new success result or continues on the error track.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccessNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="twoTrackInput"></param>
        /// <returns>Result<TSuccessNew, TFailure></returns>
        public static Result<TSuccessNew, TFailure> Map<TSuccess, TFailure, TSuccessNew>(
            this Result<TSuccess, TFailure> input,
            Func<TSuccess, Task<TSuccessNew>> twoTrackInput)
                => input.IsSuccess
                    ? Result<TSuccessNew, TFailure>.Succeeded(Task.Run(() => twoTrackInput(input.AsSuccess)).Result)
                    : Result<TSuccessNew, TFailure>.Failed(input.AsFailure);
        /// <summary>
        /// Map the failure track to a new failure result or continues on the success track.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TFailureNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="twoTrackInput"></param>
        /// <returns>Result<TSuccess, TFailureNew></returns>
        public static Result<TSuccess, TFailureNew> MapFailure<TSuccess, TFailure, TFailureNew>(
            this Result<TSuccess, TFailure> input,
            Func<TFailure, Task<Result<TSuccess, TFailureNew>>> twoTrackInput)
                => input.IsSuccess
                    ? Result<TSuccess, TFailureNew>.Succeeded(input.AsSuccess)
                    : Result<TSuccess, TFailureNew>.Failed(Task.Run(() => twoTrackInput(input.AsFailure)).Result.AsFailure);

        /// <summary>
        /// Map the success track to a new success result or continues on the error track.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccessNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="twoTrackInput"></param>
        /// <returns>Result<TSuccessNew, TFailure></returns>
        public static async Task<Result<TSuccessNew, TFailure>> MapAsync<TSuccess, TFailure, TSuccessNew>(
            this Task<Result<TSuccess, TFailure>> input,
            Func<TSuccess, TSuccessNew> twoTrackInput)
                => (await input).IsSuccess
                    ? Result<TSuccessNew, TFailure>.Succeeded(twoTrackInput(input.Result.AsSuccess))
                    : Result<TSuccessNew, TFailure>.Failed(input.Result.AsFailure);

        /// <summary>
        /// Map the success track to a new success result or continues on the error track.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccessNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="twoTrackInput"></param>
        /// <returns>Result<TSuccessNew, TFailure></returns>
        public static async Task<Result<TSuccessNew, TFailure>> MapAsync<TSuccess, TFailure, TSuccessNew>(
            this Task<Result<TSuccess, TFailure>> input,
            Func<TSuccess, Task<TSuccessNew>> twoTrackInput)
                => (await input).IsSuccess
                    ? Result<TSuccessNew, TFailure>.Succeeded(await twoTrackInput(input.Result.AsSuccess))
                    : Result<TSuccessNew, TFailure>.Failed(input.Result.AsFailure);

        /// <summary>
        /// Map the failure track to a new failure result or continues on the success track.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TFailureNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="twoTrackInput"></param>
        /// <returns>Result<TSuccess, TFailureNew></returns>
        public static async Task<Result<TSuccess, TFailureNew>> MapFailureAsync<TSuccess, TFailure, TFailureNew>(
            this Task<Result<TSuccess, TFailure>> input,
            Func<TFailure, Task<TFailureNew>> twoTrackInput)
                => (await input).IsSuccess
                    ? Result<TSuccess, TFailureNew>.Succeeded(input.Result.AsSuccess)
                    : Result<TSuccess, TFailureNew>.Failed(await twoTrackInput(input.Result.AsFailure));
        #endregion

        #region [ Tee ]
        /// <summary>
        /// Causes a side effect on the success track
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <param name="input"></param>
        /// <param name="teeAction"></param>
        /// <returns>Result<TSuccess, TFailure></returns>
        public static Task<Result<TSuccess, TFailure>> TeeAsync<TSuccess, TFailure>(
            this Task<Result<TSuccess, TFailure>> input,
            Action<TSuccess> teeAction)
        {
            if (input.Result.IsSuccess) teeAction(input.Result.AsSuccess);

            return input;
        }
        /// <summary>
        /// Causes a side effect on the error track
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <param name="input"></param>
        /// <param name="teeAction"></param>
        /// <returns>Result<TSuccess, TFailure></returns>
        public static Task<Result<TSuccess, TFailure>> TeeFailureAsync<TSuccess, TFailure>(
            this Task<Result<TSuccess, TFailure>> input,
            Action<TFailure> teeAction)
        {
            if (input.Result.IsFailure) teeAction(input.Result.AsFailure);

            return input;
        }
        #endregion
    }
}
