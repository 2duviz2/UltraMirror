using HarmonyLib;
using UltraMirror;
using UnityEngine;

namespace Mod.Patches
{
    [HarmonyPatch]
    public static class DebugPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Debug), nameof(Debug.LogError), new[] { typeof(object) })]
        public static bool LogErrorPrefix(object message)
        {
            Plugin.LogError(message.ToString());
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Debug), nameof(Debug.LogError), new[] { typeof(object), typeof(Object) })]
        public static bool LogErrorWithContextPrefix(object message, Object context)
        {
            Plugin.LogError(message.ToString());
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Debug), nameof(Debug.LogWarning), new[] { typeof(object) })]
        public static bool LogWarningPrefix(object message)
        {
            Plugin.LogWarning(message.ToString());
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Debug), nameof(Debug.LogWarning), new[] { typeof(object), typeof(Object) })]
        public static bool LogWarningWithContextPrefix(object message, Object context)
        {
            Plugin.LogWarning(message.ToString());
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Debug), nameof(Debug.Log), new[] { typeof(object) })]
        public static bool LogInfoPrefix(object message)
        {
            Plugin.LogInfo(message.ToString());
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Debug), nameof(Debug.Log), new[] { typeof(object), typeof(Object) })]
        public static bool LogInfoWithContextPrefix(object message, Object context)
        {
            Plugin.LogInfo(message.ToString());
            return false;
        }
    }
}
