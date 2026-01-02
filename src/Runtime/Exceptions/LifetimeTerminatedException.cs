using System;

namespace EniLife
{
    internal sealed class LifetimeTerminatedException : InvalidOperationException
    {
        public LifetimeTerminatedException() : base("Lifetime is terminated and cannot be used anymore!") { }
    }
}