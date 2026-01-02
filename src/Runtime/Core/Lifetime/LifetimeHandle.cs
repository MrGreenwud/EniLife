using System;
using System.Collections.Concurrent;
using System.Threading;

namespace EniLife
{
    public sealed class LifetimeHandle : IDisposable
    {
        private const int NOT_TERMINATED = 0;
        private const int TERMINATING = 1;
        private const int TERMINATED = 2;

        private readonly ConcurrentStack<Action> m_Actions = new();
        private int m_Status;

        public bool IsTerminated => Interlocked.CompareExchange(ref m_Status, 0, 0) == TERMINATED;
        public Lifetime Lifetime => new(this);

        public LifetimeHandle CreateNested()
        {
            ThrowIfTerminated();
            
            LifetimeHandle handle = new();
            OnTerminate(handle.Terminate);
            return handle;
        }

        public void OnTerminate(Action action)
        {
            ThrowIfTerminated();
            
            if(action == null) 
                throw new LifetimeNullActionException();

            m_Actions.Push(action);
        }
        public void Terminate()
        {
            if (Interlocked.CompareExchange(ref m_Status, TERMINATING, NOT_TERMINATED) != NOT_TERMINATED)
                return;

            while (m_Actions.TryPop(out var action))
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }

            m_Actions.Clear();
            Interlocked.Exchange(ref m_Status, TERMINATED);
        }

        public void Dispose() => Terminate();

        public void ThrowIfTerminated()
        {
            if(IsTerminated)
                throw new LifetimeTerminatedException();
        }
    }
}
