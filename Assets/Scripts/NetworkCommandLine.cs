using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkCommandLine : MonoBehaviour
{
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        if (Application.isEditor) {
            return;
        }

        var args = GetCommandlineArgs();

        if (args.TryGetValue("-mlapi", out string mlapiValue))
        {
            switch (mlapiValue)
            {   
                case "server":
                    networkManager.StartServer();
                    break;
                case "host":
                    networkManager.StartHost();
                    break;
                case "client":
                    networkManager.StartClient();
                    break;
            }

        }
    }

    private Dictionary<string, string> GetCommandlineArgs() {
        Dictionary<string, string> argDictionary = new Dictionary<string, string>();

        var args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i].ToLower();
            if (arg.StartsWith("-"))
            {
                var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = (value?.StartsWith("-") ?? false) ? null : value;

                argDictionary.Add(arg, value);
            }
        }
        return argDictionary;
    }
}
