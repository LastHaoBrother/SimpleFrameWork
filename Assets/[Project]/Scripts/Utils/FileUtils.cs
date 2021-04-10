using System.IO;
using System.Text;

namespace Utils
{
    public class FileUtils
    {

        public static void WriteFile(string txt, string path, bool replace = true)
        {
            //UnityEngine.Debug.Log(path);
            if (!File.Exists(path))
            {
                File.WriteAllText(path, txt, Encoding.Unicode);
            }
            else if (replace && !File.ReadAllText(path).Equals(txt))
            {
                File.WriteAllText(path, txt, Encoding.Unicode);
            }
        }

        public static void CreatDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string ReadFile(string path)
        {
            string result = string.Empty;
            if (File.Exists(path))
            {
                result = File.ReadAllText(path);
            }
            return result;

        }
    }
}