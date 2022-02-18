using System;

namespace PixelDance.Shared.ROP
{
    public static class ResultFactory
    {

        public static Result<TSuccess, TFailure> Succeeded<TSuccess, TFailure>(this TSuccess success)
        {
            _ = success ?? throw new ArgumentNullException(nameof(success));

            return new Result<TSuccess, TFailure>
                .Success(success);
        }

        public static Result<TSuccess, TFailure> Failed<TSuccess, TFailure>(this TFailure failure)
        {
            _ = failure ?? throw new ArgumentNullException(nameof(failure));

            return new Result<TSuccess, TFailure>
                .Failure(failure);
        }

    }
}
