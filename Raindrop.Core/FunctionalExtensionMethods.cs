namespace Raindrop.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FunctionalExtensionMethods
    {
        public static TOut Map<Tin, TOut>(this Tin @this, Func<Tin, TOut> func) =>
            func(@this);

        public static TInOut Tee<TInOut>(this TInOut @this, Action<TInOut> func)
        {
            func(@this);
            return @this;
        }

        public static TIn Tee<TIn, TOut>(this TIn @this, Func<TIn, TOut> func)
        {
            func(@this);
            return @this;
        }

        public static IEnumerable<TInOut> ForEach<TInOut>(this IEnumerable<TInOut> @this, Action<TInOut> func) =>
            @this.Select(x => x.Tee(func));

        public static IEnumerable<TIn> ForEach<TIn, TOut>(this IEnumerable<TIn> @this, Func<TIn, TOut> func) =>
            @this.Select(x => x.Tee(func));

        public static IReadOnlyCollection<T> Evaluate<T>(this IEnumerable<T> @this) => @this.ToList();
    }
}
