#nullable enable
#pragma warning disable CS8603 // Mögliche Nullverweisrückgabe.

using System;

namespace PixelDance.Shared.ROP
{
    public abstract record Result<TSuccess, TFailure>
    {
        public bool IsSuccess => this is Success;
        public bool IsFailure => this is Failure;

        public Success AsSuccess => this as Success;
        public Failure AsFailure => this as Failure;

        #region [ Success ]

        public record Success(TSuccess Item) : Result<TSuccess, TFailure>
        {
            public override string ToString() => $"SUCCESS: {Item?.ToString()}";

            public static implicit operator TSuccess(Success success) => success.Item;
        }

        public static Result<TSuccess, TFailure> Succeeded(TSuccess success)
        {
            _ = success ?? throw new ArgumentNullException(nameof(success));

            return new Success(success);
        }

        #endregion

        #region [ Failure ]

        public record Failure(TFailure Item) : Result<TSuccess, TFailure>
        {
            public override string ToString() => $"FAILURE: {Item?.ToString()}";

            public static implicit operator TFailure(Failure failure) => failure.Item;
        }

        public static Result<TSuccess, TFailure> Failed(TFailure failure)
        {
            _ = failure ?? throw new ArgumentNullException(nameof(failure));

            return new Failure(failure);
        }

        #endregion

    }
}
