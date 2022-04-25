using UnityEngine;

public static class SceneController
{
    public static bool GameStarted { get; private set; }

    [RuntimeInitializeOnLoadMethod]
    private static void SceneInit()
    {
        EventBus.StartGame  += StartGame;
        EventBus.EndGame    += EndGame;
        EventBus.ResetSubscribes += ResetSubscribes;
        
        MainCamera.Init();
        Score.Init();

        EventBus.OnLaunchGame();
    }
    private static void ResetSubscribes()
    {
        EventBus.StartGame  -= StartGame;
        EventBus.EndGame    -= EndGame;
        EventBus.ResetSubscribes -= ResetSubscribes;
    }

    private static void StartGame()
    {
        GameStarted = true;
    }

    private static void EndGame()
    {
        GameStarted = false;
    } 
}

