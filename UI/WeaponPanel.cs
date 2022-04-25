public class WeaponPanel : PanelUI
{
    public override void OnEnable()
    {
        base.OnEnable();
        EventBus.StartGame += ActivateChildrenPanel;
        EventBus.EndGame += DeactivateChildrenPanel;
        EventBus.LaunchGame += DeactivateChildrenPanel;
    }

    public override void OnDisable()
    {
        EventBus.StartGame -= ActivateChildrenPanel;
        EventBus.EndGame -= DeactivateChildrenPanel;
        EventBus.LaunchGame -= DeactivateChildrenPanel;
    }
}
