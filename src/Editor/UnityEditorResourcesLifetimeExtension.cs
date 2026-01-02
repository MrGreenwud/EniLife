using System;
using UnityEngine;
using EniLife;

namespace EniLife.Editor
{
    public static class UnityEditorResourcesLifetimeExtension
    {
        public static LifetimeHandle EditorAutoDispose(this RenderTexture renderTexture)
        {
            return renderTexture.AutoDispose(EditorLifetime.Global);
        }
        public static LifetimeHandle EditorAutoDispose(this UnityEngine.Object resource)
        {
            return resource.AutoDispose(EditorLifetime.Global);
        }
        public static LifetimeHandle EditorAutoDispose(this GameObject gameObject)
        {
            return gameObject.AutoDispose(EditorLifetime.Global);
        }

        public static LifetimeHandle EditorAutoDispose<T>(this T resource, Action<T> dispose)
        {
            return resource.AutoDispose(EditorLifetime.Global, dispose);
        }
    }
}
