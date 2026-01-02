using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace EniLife
{
    public sealed class SignalHandle : IDisposable
    {
        private readonly List<Action> m_Subscribers = new();
        private readonly List<Action> m_SubscribeQueue = new();
        private readonly List<Action> m_UnsubscribeQueue = new();

        private bool m_IsFiring;
        private bool m_IsDisposed;

        public Signal Signal => new Signal(this);

        public void Fire()
        {
            ThrowIfDisposed();

            m_IsFiring = true;

            for (int i = 0; i < m_Subscribers.Count; i++)
            {
                try
                {
                    m_Subscribers[i]?.Invoke();
                }
                catch(Exception e) 
                {
                    UnityEngine.Debug.LogException(e);
                }
            }

            for (int i = 0; i < m_SubscribeQueue.Count; i++)
                m_Subscribers.Add(m_SubscribeQueue[i]);

            for (int i = 0; i < m_UnsubscribeQueue.Count; i++)
                m_Subscribers.Remove(m_UnsubscribeQueue[i]);

            m_SubscribeQueue.Clear();
            m_UnsubscribeQueue.Clear();

            m_IsFiring = false;
        }
        public void Subscribe(Lifetime lifetime, Action subscriber)
        {
            lifetime.ThrowIfTerminated();
            ThrowIfDisposed();

            if (subscriber == null)
                throw new SignalNullSubscriberException();

            if (m_IsFiring)
                m_SubscribeQueue.Add(subscriber);
            else
                m_Subscribers.Add(subscriber);

            lifetime.OnTerminate(() =>
            {
                if (m_IsDisposed)
                    return;

                if (m_IsFiring)
                    m_UnsubscribeQueue.Add(subscriber);
                else
                    m_Subscribers.Remove(subscriber);
            });
        }
        
        public void Dispose()
        {
            m_IsDisposed = true;

            m_Subscribers.Clear();
            m_SubscribeQueue.Clear();
            m_UnsubscribeQueue.Clear();

            m_IsFiring = false;
        }

        [Conditional("DEBUG")]
        private void ThrowIfDisposed()
        {
            if(m_IsDisposed)
                throw new SignalDisposedException();
        }
    }

    
}
