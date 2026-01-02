using UnityEngine;

namespace EniLife
{
    public sealed class MonoLifetime : MonoBehaviour
    {
        private LifetimeHandle m_Handle = new();
        
        public Lifetime Lifetime => m_Handle.Lifetime;

        private void OnDestroy() => m_Handle.Terminate();
    }

    
}
