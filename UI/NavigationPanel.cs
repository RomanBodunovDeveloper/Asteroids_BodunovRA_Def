using UnityEngine;
using UnityEngine.UI;

public class NavigationPanel : PanelUI
{
    private Text textPositionX;
    private Text textPositionY;
    private Text textRotation;
    private Text textSpeed;
    public override void OnEnable()
    {
        base.OnEnable();

        textPositionX = InitChildPanelText("PositionX");
        textPositionY = InitChildPanelText("PositionY");
        textRotation  = InitChildPanelText("Rotation");
        textSpeed     = InitChildPanelText("Speed");

        EventBus.StartGame  += ActivateChildrenPanel;
        EventBus.EndGame    += DeactivateChildrenPanel;
        EventBus.LaunchGame += DeactivateChildrenPanel;
        EventBus.StarshipNavigationChange += UpdateNavigation;
    }

    public override void OnDisable()
    {
        EventBus.StarshipNavigationChange -= UpdateNavigation;
        EventBus.StartGame  -= ActivateChildrenPanel;
        EventBus.EndGame    -= DeactivateChildrenPanel;
        EventBus.LaunchGame -= DeactivateChildrenPanel;
    }

    private void UpdateNavigation(Vector3 position, Quaternion rotation, float speed)
    {
        textPositionX.text = "X:" + position.x.ToString("F2");
        textPositionY.text = "Y:" + position.y.ToString("F2");
        textRotation.text  = rotation.eulerAngles.z.ToString("F1") + "°";
        textSpeed.text     = speed.ToString("F2") + "kn";
    }

}
