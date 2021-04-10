internal class GameScene : BaseScene
{
    public override string SceneName => "Game";

    protected internal override void OnEnter()
    {
        //AppContext.vMagager.Show<ModelTestView>();
        AppContext.vMagager.Show<UITestPanelView>();
    }

    protected internal override void OnLeave()
    {
        base.OnLeave();
    }


}
