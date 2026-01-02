using System;
using UnityEngine;

namespace EniLife
{
    public static class UnityResourcesLifetimeExtension
    {
        public static LifetimeHandle AutoDispose(this RenderTexture renderTexture)
        {
            return renderTexture.AutoDispose(RuntimeLifetime.Global);
        }
        public static LifetimeHandle AutoDispose(this UnityEngine.Object resource)
        {
            return resource.AutoDispose(RuntimeLifetime.Global);
        }
        public static LifetimeHandle AutoDispose(this GameObject gameObject)
        {
            return gameObject.AutoDispose(RuntimeLifetime.Global);
        }

        public static LifetimeHandle AutoDispose(this RenderTexture renderTexture, Lifetime lifetime)
        {
            return AutoDispose(renderTexture, lifetime, (x) => { x.Dispose(); });
        }
        public static LifetimeHandle AutoDispose(this UnityEngine.Object resource, Lifetime lifetime)
        {
            return AutoDispose(resource, lifetime, (x) => { x.Dispose(); });
        }
        public static LifetimeHandle AutoDispose(this GameObject gameObject, Lifetime lifetime)
        {
            return AutoDispose(gameObject, lifetime, (x) => { x.Dispose(); });
        }

        public static LifetimeHandle AutoDispose<T>(this T resource, Action<T> dispose)
        {
            return resource.AutoDispose(RuntimeLifetime.Global, dispose);
        }
        public static LifetimeHandle AutoDispose<T>(this T resource, Lifetime lifetime, Action<T> dispose)
        {
            T captured = resource;
            var handle = lifetime.CreateNested();
            handle.Lifetime.OnTerminate(() => { dispose(captured); });

            return handle;
        }
    }

    
}
