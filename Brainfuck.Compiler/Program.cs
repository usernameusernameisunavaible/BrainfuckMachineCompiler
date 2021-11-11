namespace Brainfuck.Com
{
    using System.IO;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Compiler compiler = new();
            File.WriteAllBytes(args[0], compiler.Compile(File.ReadAllText(args[1])));
        }
    }
}