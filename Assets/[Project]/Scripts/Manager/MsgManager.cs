using System.Collections.Generic;

public class MsgManager : BaseModule<MsgManager>
{


    internal void AddListenner(string testMsg)
    {
        throw new System.NotImplementedException();
    }

    //public delegate void EventHandler(MsgPackage package);
    public delegate void EventHandler(object p);
    private Dictionary<string, List<EventHandler>> msgDic;



    public override void Init()
    {
        base.Init();
        msgDic = new Dictionary<string, List<EventHandler>>();
    }

    public void AddListenner(string msgName, EventHandler eventHandler)
    {
        if (msgDic.ContainsKey(msgName))
        {
            msgDic[msgName].Add(eventHandler);
        }
        else
        {
            List<EventHandler> list = new List<EventHandler>();
            list.Add(eventHandler);
            msgDic.Add(msgName, list);
        }
    }

    public void RemoveListener(string msgName)
    {
        if (msgDic.ContainsKey(msgName))
        {
            msgDic[msgName].Clear();
            msgDic.Remove(msgName);
        }
    }

    public void RemoveListener(string msgName, EventHandler eventHandler)
    {
        if (msgDic.ContainsKey(msgName))
        {
            if (msgDic[msgName].Contains(eventHandler))
            {
                msgDic[msgName].Remove(eventHandler);
            }
            //if (msgDic[msgName].Count == 0)
            //{
            //    msgDic.Remove(msgName);
            //}
        }
    }

    public void SendMsg(string msgName, object msg)
    {
        if (msgDic.ContainsKey(msgName))
        {
            foreach (var item in msgDic[msgName])
            {
                item(msg);
            }
        }
    }

    //public void SendMsg(string msgName, MsgPackage msg)
    //{
    //    if (msgDic.ContainsKey(msgName))
    //    {
    //        foreach (var item in msgDic[msgName])
    //        {
    //            item(msg);
    //        }
    //    }
    //}

    public override void Dispose()
    {
        base.Dispose();
        List<string> keys = new List<string>(msgDic.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            RemoveListener(keys[i]);
        }
        //foreach (var item in msgDic)
        //{
        //    RemoveListener(item.Key);
        //}
        msgDic.Clear();
    }

}

/// <summary>
/// 消息包
/// </summary>
public class MsgPackage
{
    public object Params { get; set; }
}
