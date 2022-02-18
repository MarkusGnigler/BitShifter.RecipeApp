using System;

namespace PixelDance.Shared.ROP
{
    public abstract class Result<TSuccess, TFailure>
    {
        internal bool IsSuccess => this is Success;
        internal bool IsFailure => this is Failure;

        internal Success AsSuccees()
            => this as Success
                ?? throw new NullReferenceException(nameof(Success));
        internal Failure AsFailure()
            => this as Failure
                ?? throw new NullReferenceException(nameof(Failure));

        #region [ Success ]

        public class Success : Result<TSuccess, TFailure>
        {
            internal readonly TSuccess _item;

            internal Success(TSuccess success)
            {
                _item = success
                    ?? throw new ArgumentNullException(nameof(success));
            }

            public override string ToString() => $"SUCCESS: {_item}";

            public static implicit operator TSuccess(Success success) => success._item;
        }

        internal static Result<TSuccess, TFailure> Succeeded(TSuccess success)
            => new Success(success);

        #endregion

        #region [ Failure ]

        public class Failure : Result<TSuccess, TFailure>
        {
            internal readonly TFailure _item;

            internal Failure(TFailure failure)
            {
                _item = failure
                    ?? throw new ArgumentNullException(nameof(failure));
            }

            public override string ToString() => $"FAILURE: {_item}";

            public static implicit operator TFailure(Failure failure) => failure._item;
        }

        internal static Result<TSuccess, TFailure> Failed(TFailure failure)
            => new Failure(failure);

        #endregion

    }
}
