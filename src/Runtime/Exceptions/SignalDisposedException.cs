using System;

namespace EniLife
{
    internal sealed class SignalDisposedException : InvalidOperationException
    {
        public SignalDisposedException() : base("Cannot use a disposed signal!") { }
    }
}