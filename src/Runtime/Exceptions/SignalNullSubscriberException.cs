using System;

namespace EniLife
{
    internal sealed class SignalNullSubscriberException : ArgumentNullException
    {
        public SignalNullSubscriberException() : base("Subscriber cannot be null!") { }
    }
}