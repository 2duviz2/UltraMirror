namespace UltraMirror;

using BepInEx;
using GameConsole;
using GameConsole.Commands;
using HarmonyLib;
using Mirror;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
    public static Plugin instance;

    static NetworkManager networkManager;

    public void Awake()
    {
        new Harmony(PluginInfo.GUID).PatchAll();

        gameObject.hideFlags = HideFlags.HideAndDontSave;
        instance = this;

        gameObject.AddComponent<BundlesManager>();
    }

    public void Start()
    {
        GameObject g = Instantiate(BundlesManager.bundle.LoadAsset<GameObject>("NetworkManager"));
        networkManager = g.GetComponent<NetworkManager>();
        DontDestroyOnLoad(g);

        RegisterCommands();
    }

    public void Update()
    {
        
    }

    public static T Ass<T>(string path)
    {
        return Addressables.LoadAssetAsync<T>((object)path).WaitForCompletion();
    }

    public static void StartHostOnScene(string sceneName)
    {
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single).Completed += handle =>
        {
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                networkManager.StartHost();
                networkManager.autoCreatePlayer = true;

                GameObject player = Instantiate(networkManager.playerPrefab);
                NetworkServer.AddPlayerForConnection(NetworkServer.localConnection, player);

                LogInfo("Started hosting after scene load...");
            }
            else
            {
                LogError("Failed to load scene: " + sceneName);
            }
        };
    }

    public static void CreateLobby()
    {
        StartHostOnScene("uk_construct");
        LogInfo("Hosting lobby...");
    }

    public static void JoinLobby(string ip)
    {
        networkManager.networkAddress = ip;
        networkManager.StartClient();
        LogInfo("Joining lobby...");
    }

    public static void RegisterCommands()
    {
        Console.Instance.RegisterCommand(new CreateLobbyCommand());
        Console.Instance.RegisterCommand(new JoinLobbyCommand());
    }

    public static void LogInfo(object msg) { instance.Logger.LogInfo(msg); }
    public static void LogWarning(object msg) { instance.Logger.LogWarning(msg); }
    public static void LogError(object msg) { instance.Logger.LogError(msg); }
}

public class PluginInfo
{
    public const string GUID = "duviz.UltraMirror";
    public const string Name = "UltraMirror";
    public const string Version = "1.0.0";
}