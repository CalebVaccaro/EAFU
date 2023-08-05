# Easy Azure Functions for Unity (EAFU)

## About

Easy Azure Functions for Unity (EAFU) is a lightweight library designed to make it easier for Unity developers to interface with Azure Functions. It provides base classes and a service class to simplify sending HTTP requests to Azure Functions.

EAFU is designed to be customizable, enabling developers to easily create specific APIs based on the provided base classes.



## Structure
EAFU consists of the following main components:

1. `ApiService`: The core class that performs the HTTP requests to Azure Functions. It provides GET, POST, PUT, and DELETE methods and invokes custom actions to handle errors and loading states.

2. `EAFUApi`: A base class for creating custom APIs. You can inherit this class and customize it to suit your needs.

3. `ApiEndpoints`: A simple structure for holding the URIs for each CRUD operation (GET, POST, PUT, DELETE) for a specific API. Each URI should be the path to a distinct Azure Function.

4. `EAFU`: Monobehaviour Gameobject able to set the error and success actions for responses.

## How to use EAFU

1. First, set up your Azure Functions (see [Azure Functions documentation](https://docs.microsoft.com/en-us/azure/azure-functions/) for guidance).

2. Add `local.settings.json` file or remote key configuration
    - Local Development Configuration: 
        - Create a `local.settings.json` file to configure your application. This file includes AZURE_FUNCTION_APP_URL and AZURE_FUNCTION_APP_KEY.
        - The format should be:
        ```json
        {
            "AZURE_FUNCTION_APP_URL" : "URL",
            "AZURE_FUNCTION_APP_KEY" : "KEY"
        }
        ```
    - Prod Development Configuration: 
        - See [Unity Remote Config](https://docs.unity3d.com/Packages/com.unity.remote-config@3.3/manual/index.html)

3. Implement your custom APIs inheriting from `EAFUApi` class and specify your Azure Function endpoints in the `ApiEndpoints` object. The `ApiEndpoints` object holds the URIs for each CRUD operation (GET, POST, PUT, DELETE) for a specific API.

4. Initialize `ApiService.BaseUrl` to your Azure Function App's base URL.

5. Use your custom APIs to communicate with Azure Functions. `EAFU` will handle the request and response parsing, errors, and loading states.

```csharp
[Serializable]
public class GameApi : EAFUApi
{
    public Player player { get; set; }

    public void CreatePlayer(string name, int score, int gameDuration, Action<object> PostPlayerAction)
    {
        player = new Player(name, score, gameDuration);
        Post(player, PostPlayerAction);
    }

    public void GetLeaderboard(Action<List<Player>> GetLeaderboardsAction) => Get(GetLeaderboardsAction);
}
```

## Creating Azure Functions

For a step by step guide on creating and publishing your first Azure Function, please refer to the official Microsoft Azure documentation [here](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp).

---

Please note that this is a high-level guide, and it's recommended to be familiar with Azure Functions and Unity development. Enjoy using EAFU!
