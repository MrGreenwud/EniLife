using System.Threading;
using System;

namespace EniLife
{
    public static class LifetimeExtensions
    {
        public static void AutoDispose(this Lifetime lifetime, IDisposable disposable)
        {
            lifetime.OnTerminate(disposable.Dispose);
        }
        public static T AutoDispose<T>(this T disposable, Lifetime lifetime) where T : IDisposable
        {
            lifetime.OnTerminate(disposable.Dispose);
            return disposable;
        }

        public static void Bracket(this Lifetime lifetime, Action acquire, Action release)
        {
            lifetime.ThrowIfTerminated();
            acquire();
            lifetime.OnTerminate(release);
        }

        public static CancellationToken ToCancellationToken(this Lifetime lifetime)
        {
            if (lifetime.IsTerminated)
                return CancellationToken.None;

            var cts = new CancellationTokenSource();
            lifetime.OnTerminate(cts.Cancel);
            return cts.Token;
        }
    }
}