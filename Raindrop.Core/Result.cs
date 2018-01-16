namespace Raindrop.Utility
{
    using System;

    public class Result
    {
        public bool Success { get; }
        public string Message { get; }
        public Exception Exception { get; }

        protected Result(bool success, string message, Exception exception)
        {
            Success = success;
            Message = message;
            Exception = exception;
        }

        public static Result Succeed() =>
            new Result(true, string.Empty, null);

        public static Result Fail(string message) =>
            new Result(false, message, null);

        public static Result Fail(Exception exception) =>
            new Result(false, string.Empty, exception);

        public static Result Fail(string message, Exception exception) =>
            new Result(false, message, exception);

        public Result Bind(Func<Result> func) =>
            Success
            ? func()
            : this;

        public Result<T> Bind<T>(Func<Result<T>> func) =>
            Success
            ? func()
            : Result<T>.Fail(Message, Exception);

        public Result<T> Bind<T>(Func<T> func) =>
            Success
            ? TryFunc(func)
            : Result<T>.Fail(Message, Exception);

        private static Result<T> TryFunc<T>(Func<T> func)
        {
            try { return func().Map(Result<T>.Succeed); }
            catch (Exception ex) { return Result<T>.Fail(ex); }
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(bool success, T value, string message, Exception exception)
            : base(success, message, exception)
        {
            Value = value;
        }

        public static Result<T> Succeed(T value) =>
            new Result<T>(true, value, string.Empty, null);

        public new static Result<T> Fail(string message) =>
            new Result<T>(false, default(T), message, null);

        public new static Result<T> Fail(Exception exception) =>
            new Result<T>(false, default(T), string.Empty, exception);

        public new static Result<T> Fail(string message, Exception exception) =>
            new Result<T>(false, default(T), message, exception);

        public Result Bind(Func<T, Result> func) =>
            Success
            ? func(Value)
            : this;

        public Result<TOut> Bind<TOut>(Func<T, Result<TOut>> func) =>
            Success
            ? func(Value)
            : new Result<TOut>(false, default(TOut), Message, null);
    }
}
