using UnityEngine;

namespace EniLife
{
    public static class UnityResourcesDisposeExtension
    {
        public static void Dispose(this RenderTexture renderTexture)
        {
            renderTexture.Release();
            Object.DestroyImmediate(renderTexture);
        }
        public static void Dispose(this UnityEngine.Object resource)
        {
            Object.DestroyImmediate(resource);
        }
        public static void Dispose(this GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}
