using System;

namespace EniLife
{
    public readonly struct Signal
    {
        private readonly SignalHandle m_Handle;

        public Signal(SignalHandle handle)
        {
            m_Handle = handle;
        }

        public void Subscribe(Lifetime lifetime, Action subscriber) => m_Handle.Subscribe(lifetime, subscriber);
    }
}
