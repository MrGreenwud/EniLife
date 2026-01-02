using System;

namespace EniLife
{
    internal sealed class LifetimeNullActionException : ArgumentNullException
    {
        public LifetimeNullActionException() : base("Cannot subscribe a null action!") { }
    }
}