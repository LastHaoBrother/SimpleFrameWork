using System.IO;
using System.Xml;

public class XmlUtils
{
    public static string ReadNode(string path, string nodeName)
    {
        string result = "error";
        try
        {
            if (File.Exists(path) && !string.IsNullOrEmpty(nodeName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNode xNode = xmlDoc.GetElementsByTagName(nodeName)[0];
                result = xNode.InnerText;
            }
        }
        catch (System.Exception e)
        {
            result = e.Message;
        }
        return result;
    }
}
