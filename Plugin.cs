namespace Mod;

using BepInEx;
using HarmonyLib;

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
    public void Awake()
    {
        new Harmony(PluginInfo.GUID).PatchAll();
    }
}

public class PluginInfo
{
    public const string GUID = "AuthorName.ModName";
    public const string Name = "ModName";
    public const string Version = "1.0.0";
}