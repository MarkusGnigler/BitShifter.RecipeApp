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
                    ? Task.Run(() => switchFunction(input.AsSuccees()))
                        .TaskAwaiter()
                    : Result<TSuccessNew, TFailure>.Failed(input.AsFailure());

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
                    ? await switchFunction(input.Result.AsSuccees())
                    : Result<TSuccessNew, TFailure>.Failed(input.Result.AsFailure());

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
                    ? Result<TSuccessNew, TFailure>.Succeeded(Task.Run(() => twoTrackInput(input.AsSuccees())).TaskAwaiter())
                    : Result<TSuccessNew, TFailure>.Failed(input.AsFailure());

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
                => input.Result.IsSuccess
                    ? Result<TSuccessNew, TFailure>.Succeeded(twoTrackInput((await input).AsSuccees()))
                    : Result<TSuccessNew, TFailure>.Failed(input.Result.AsFailure());

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
                => input.Result.IsSuccess
                    ? Result<TSuccessNew, TFailure>.Succeeded(await twoTrackInput((await input).AsSuccees()))
                    : Result<TSuccessNew, TFailure>.Failed(input.Result.AsFailure());

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
            Func<TFailure, Task<TFailureNew>> twoTrackInput)
                => input.IsSuccess
                    ? Result<TSuccess, TFailureNew>.Succeeded(input.AsSuccees())
                    : Result<TSuccess, TFailureNew>.Failed(Task.Run(() => twoTrackInput(input.AsFailure())).TaskAwaiter());

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
                => input.Result.IsSuccess
                    ? Result<TSuccess, TFailureNew>.Succeeded(input.Result.AsSuccees())
                    : Result<TSuccess, TFailureNew>.Failed((await twoTrackInput((await input).AsFailure())));

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
        public static Task<Result<TSuccess, TFailure>> Tee<TSuccess, TFailure>(
            this Task<Result<TSuccess, TFailure>> input,
            Action<TSuccess> teeAction)
        {
            if (input.TaskAwaiter().IsSuccess) teeAction(input.TaskAwaiter().AsSuccees());

            return input;
        }

        /// <summary>
        /// Causes a side effect on the success track
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <param name="input"></param>
        /// <param name="teeAction"></param>
        /// <returns>Result<TSuccess, TFailure></returns>
        public static async Task<Result<TSuccess, TFailure>> TeeAsync<TSuccess, TFailure>(
            this Task<Result<TSuccess, TFailure>> input,
            Action<TSuccess> teeAction)
        {
            var value = await input;

            if (value.IsSuccess)
                teeAction(value.AsSuccees());

            return value;
        }

        /// <summary>
        /// Causes a side effect on the error track
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <param name="input"></param>
        /// <param name="teeAction"></param>
        /// <returns>Result<TSuccess, TFailure></returns>
        public static Task<Result<TSuccess, TFailure>> TeeFailure<TSuccess, TFailure>(
            this Task<Result<TSuccess, TFailure>> input,
            Action<TFailure> teeAction)
        {
            if (input.TaskAwaiter().IsFailure)
                teeAction(input.TaskAwaiter().AsFailure());

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
        public static async Task<Result<TSuccess, TFailure>> TeeFailureAsync<TSuccess, TFailure>(
            this Task<Result<TSuccess, TFailure>> input,
            Action<TFailure> teeAction)
        {
            var value = await input;

            if (value.IsFailure)
                teeAction(value.AsFailure());

            return value;
        }

        #endregion

        #region [ Match ]

        /// <summary>
        /// Get a result from the specific track
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="twoTrackInput"></param>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns>TOutput</returns>
        public static async Task<TOutput> MatchAsync<TSuccess, TFailure, TOutput>(
            this Task<Result<TSuccess, TFailure>> twoTrackInput,
            Func<TSuccess, TOutput> onSuccess,
            Func<TFailure, TOutput> onFailure)
                => (await twoTrackInput).IsSuccess
                    ? onSuccess((await twoTrackInput).AsSuccees())
                    : onFailure((await twoTrackInput).AsFailure());
        #endregion

        #region [ Private ]

        private static TResult TaskAwaiter<TResult>(this Task<TResult> task)
            => task
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        #endregion

    }
}
