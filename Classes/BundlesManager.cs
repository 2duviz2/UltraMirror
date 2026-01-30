namespace UltraMirror;

using System.IO;
using System.Reflection;
using UnityEngine;

public class BundlesManager : MonoBehaviour
{
    public static AssetBundle bundle;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        var assembly = Assembly.GetExecutingAssembly();

        string resourceName = "Mod.Assets.ultramirror.bundle";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                Plugin.LogError($"Embedded resource '{resourceName}' not found!");
                return;
            }

            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            bundle = AssetBundle.LoadFromMemory(data);
            if (bundle != null)
                Plugin.LogInfo("Loaded embedded AssetBundle!");
            else
                Plugin.LogError("Failed to load AssetBundle from memory!");
        }
    }

    public void OnDestroy()
    {
        bundle?.Unload(false);
    }
}