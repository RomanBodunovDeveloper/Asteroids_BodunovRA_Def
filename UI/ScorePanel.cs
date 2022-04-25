using UnityEngine.UI;

public class ScorePanel : PanelUI
{
    private Text TitleText;
    public override void OnEnable()
    {
        base.OnEnable();

        TitleText = InitChildPanelText("Title");

        EventBus.UpdateScore += UpdateScoreText;
        EventBus.StartGame   += ActivateChildrenPanel;
        EventBus.EndGame     += DeactivateChildrenPanel;
        EventBus.LaunchGame  += DeactivateChildrenPanel;
    }

    private void UpdateScoreText(int curScore)
    {
        TitleText.text = "SCORE: " + curScore;
    }
}
