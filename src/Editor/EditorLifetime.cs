using UnityEditor;

namespace EniLife.Editor
{
    public static class EditorLifetime
    {
        private static readonly LifetimeHandle s_Handle = new();

        public static Lifetime Global => s_Handle.Lifetime;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            AssemblyReloadEvents.beforeAssemblyReload += () => { s_Handle.Dispose(); };
        }
    }
}