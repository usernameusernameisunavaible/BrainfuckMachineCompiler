using System.IO;

namespace BrainfuckMachineCompiler
{
    //is just to implement a file -reader/-writer more easily
    class FileAbstractions
    {
        public static void SaveText(string path, string text)
        {
            FileInfo f = new(path);
            if(!f.Exists)
            {
                File.Create(path).Close();
            }
            StreamWriter sw = new(path);
            sw.Write(text);
            sw.Close();

        }

        public static void SaveBytes(string path, byte[] bytes)
        {
            FileInfo f = new(path);
            if (!f.Exists)
            {
                File.Create(path).Close();
            }
            File.WriteAllBytes(path, bytes);
        }

        public static string LoadText(string path)
        {
            FileInfo f = new(path);
            if(f.Exists)
            {
                StreamReader sr = new(path);
                string text = sr.ReadToEnd();
                sr.Close();
                return text;
            }
            return "ERROR\0WHILE\0READING";
        }
    }
}
