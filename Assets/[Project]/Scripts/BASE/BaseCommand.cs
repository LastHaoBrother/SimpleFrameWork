
public class BaseCommand : object
{
    private string commandDescribe;
    public string CommandDescribe { get => commandDescribe; set => commandDescribe = value; }


    private CommandType commandType;

    public BaseCommand(CommandType commandType)
    {
        CommandType = commandType;
    }

    public CommandType CommandType { get => commandType; set => commandType = value; }


    /// <summary>
    /// 执行操作
    /// </summary>
    public virtual void Do() { }
    /// <summary>
    /// 撤销操作
    /// </summary>
    public virtual void UnDo() { }
}

