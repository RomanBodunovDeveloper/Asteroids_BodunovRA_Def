using UnityEngine.UI;

public class InfoPanel : PanelUI
{
    private Text TitleText;
    private Text SubtitleText;
    public override void OnEnable()
    {
        base.OnEnable();

        TitleText    = InitChildPanelText("Title");
        SubtitleText = InitChildPanelText("Subtitle");

        EventBus.SendEndGameInfo += ShowEndGameInfo;
        EventBus.LaunchGame      += ShowLaunchGameInfo;
        EventBus.StartGame       += DeactivateChildrenPanel;
    }

    public override void OnDisable()
    {
        EventBus.SendEndGameInfo -= ShowEndGameInfo;
        EventBus.LaunchGame      -= ShowLaunchGameInfo;
        EventBus.StartGame       -= DeactivateChildrenPanel;
    }

    private void ShowEndGameInfo(int score)
    {
        ActivateChildrenPanel();
        TitleText.text    = "YOU SCORE: " + score;
        SubtitleText.text = "For start press ENTER";
    }

    private void ShowLaunchGameInfo()
    {
        ActivateChildrenPanel();
        TitleText.text    = "ASTEROIDS GAME";
        SubtitleText.text = "For start press ENTER\n\nFor Starship move press W-A-D\nFor Starship strike press SPACE-E";
    }
}
