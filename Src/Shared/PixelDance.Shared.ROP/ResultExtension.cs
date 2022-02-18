using System;

namespace PixelDance.Shared.ROP
{
    public static class ResultExtension
    {

        #region [ Bind ]

        /// <summary>
        /// Takes a Result Monad and creates a new Result Monad.
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <typeparam name="TSuccessNew"></typeparam>
        /// <param name="input"></param>
        /// <param name="switchFunction"></param>
        /// <returns>Result<TSuccessNew, TFailure></returns>
        public static Result<TSuccessNew, TFailure> Bind<TSuccess, TFailure, TSuccessNew>(
            this Result<TSuccess, TFailure> input,
            Func<TSuccess, Result<TSuccessNew, TFailure>> switchFunction)
                => input.IsSuccess
                    ? switchFunction(input.AsSuccees())
                    : Result<TSuccessNew, TFailure>.Failed(input.AsFailure());

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
            Func<TSuccess, TSuccessNew> twoTrackInput)
                => input.IsSuccess
                    ? Result<TSuccessNew, TFailure>.Succeeded(twoTrackInput(input.AsSuccees()))
                    : Result<TSuccessNew, TFailure>.Failed(input.AsFailure());
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
            Func<TFailure, TFailureNew> twoTrackInput)
                => input.IsSuccess
                    ? Result<TSuccess, TFailureNew>.Succeeded(input.AsSuccees())
                    : Result<TSuccess, TFailureNew>.Failed(twoTrackInput(input.AsFailure()));


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
        public static Result<TSuccess, TFailure> Tee<TSuccess, TFailure>(
            this Result<TSuccess, TFailure> input,
            Action<TSuccess> teeAction)
        {
            if (input.IsSuccess) teeAction(input.AsSuccees());

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
        public static Result<TSuccess, TFailure> TeeFailure<TSuccess, TFailure>(
            this Result<TSuccess, TFailure> input,
            Action<TFailure> teeAction)
        {
            if (input.IsFailure) teeAction(input.AsFailure());

            return input;
        }
        #endregion

        #region [ Either ]

        public static Result<TSuccess2, TFailure2> Either<TSuccess, TFailure, TSuccess2, TFailure2>(
            this Result<TSuccess, TFailure> input,
            Func<Result<TSuccess, TFailure>, Result<TSuccess2, TFailure2>> onSuccess,
            Func<Result<TSuccess, TFailure>, Result<TSuccess2, TFailure2>> onFailure)
                => input.IsSuccess ? onSuccess(input) : onFailure(input);

        public static Result<TSuccess, TFailure[]> ToFailure<TSuccess, TFailure>(
            this Result<TSuccess, TFailure[]> input)
                => input.Either(
                        s => Result<TSuccess, TFailure[]>.Failed(Array.Empty<TFailure>()),
                        f => f
                    );

        #endregion

        #region [ Dead End ]

        /// <summary>
        /// Handles the result in the specific Action
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TFailure"></typeparam>
        /// <param name="result"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailure"></param>
        public static void Handle<TSuccess, TFailure>(
            this Result<TSuccess, TFailure> result,
            Action<TSuccess> onSuccess,
            Action<TFailure> onFailure)
        {
            if (result.IsSuccess)
                onSuccess(result.AsSuccees());
            else
                onFailure(result.AsFailure());
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
        public static TOutput Match<TSuccess, TFailure, TOutput>(
            this Result<TSuccess, TFailure> twoTrackInput,
            Func<TSuccess, TOutput> onSuccess,
            Func<TFailure, TOutput> onFailure)
                => twoTrackInput.IsSuccess
                    ? onSuccess(twoTrackInput.AsSuccees())
                    : onFailure(twoTrackInput.AsFailure());

        public static Func<TInput, TOutput> Match<TInput, TSuccess, TFailure, TOutput>(
            this Func<TInput, Result<TSuccess, TFailure>> twoTrackInputFunction,
            Func<TSuccess, TOutput> onSuccess,
            Func<TFailure, TOutput> onFailure)
                => input => twoTrackInputFunction(input).Match(onSuccess, onFailure);

        #endregion

    }
}
