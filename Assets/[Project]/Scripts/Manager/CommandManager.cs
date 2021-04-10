using System.Collections.Generic;

public class CommandManager : BaseModule<CommandManager>
{

    private List<BaseCommand> CommandList;

    public override void Init()
    {
        base.Init();
        CommandList = new List<BaseCommand>();
    }



    public override void Dispose()
    {
        base.Dispose();
        CommandList.Clear();
        CommandList = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="record">是否记录操作</param>
    public void DoCommand(BaseCommand command, bool record = true)
    {
        command.Do();
        if (record) CommandList.Add(command);
    }

    public void UnDoCommand()
    {
        if (CommandList.Count > 0)
        {
            BaseCommand command = CommandList[CommandList.Count - 1];
            command.UnDo();
            CommandList.Remove(command);
        }
    }

    public void ClearCommandList()
    {
        if (CommandList != null)
            CommandList.Clear();
    }
}
