using UnityEngine;

public static class Score
{
    private static int curScore;

    public static void Init()
    {
        EventBus.EnemyDestroyed  += IncreaseCurScore;
        EventBus.StartGame       += ResetCurScore;
        EventBus.EndGame         += SendEndGameScore;
        EventBus.ResetSubscribes += ResetSubscribes;
    }

    private static void ResetSubscribes()
    {
        EventBus.EnemyDestroyed  -= IncreaseCurScore;
        EventBus.StartGame       -= ResetCurScore;
        EventBus.EndGame         -= SendEndGameScore;
        EventBus.ResetSubscribes -= ResetSubscribes;
    }
    private static void IncreaseCurScore(GameObject enemyObject, int score)
    {
        curScore += score;
        EventBus.OnUpdateScore(curScore);
    }

    private static void ResetCurScore()
    {
        curScore = 0;
        EventBus.OnUpdateScore(curScore);
    }

    private static void SendEndGameScore()
    {
        EventBus.OnSendEndGameInfo(curScore);
    }
}
