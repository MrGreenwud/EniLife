using UnityEngine;

namespace EniLife
{
    public static class RuntimeLifetime
    {
        private static MonoLifetime s_Handle;

        public static Lifetime Global => s_Handle.Lifetime;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            GameObject handleObject = new("[Runtime_Lifetime_Handle]");
            GameObject.DontDestroyOnLoad(handleObject);
            s_Handle = handleObject.AddComponent<MonoLifetime>();
        }
    }
}
