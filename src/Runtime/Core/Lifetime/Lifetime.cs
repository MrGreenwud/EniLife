using System;

namespace EniLife
{
    public readonly struct Lifetime
    {
        public static Lifetime Eternal => new(null);

        private readonly LifetimeHandle m_Handle;

        public bool IsTerminated => m_Handle?.IsTerminated ?? false;

        public Lifetime(LifetimeHandle handle)
        {
            m_Handle = handle;
        }

        public readonly LifetimeHandle CreateNested() => m_Handle?.CreateNested() ?? new();
        public readonly void OnTerminate(Action action) => m_Handle?.OnTerminate(action);

        public readonly void ThrowIfTerminated() => m_Handle?.ThrowIfTerminated();
    }

    
}
