
public abstract class BaseScene
{
    public abstract string SceneName { get; }

    public bool active { get; set; }

    protected internal virtual void OnEnter()
    {

    }

    protected internal virtual void OnLeave()
    {

    }

}
