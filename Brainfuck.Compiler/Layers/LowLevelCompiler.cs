namespace Brainfuck.Com.Layers
{
    using Brainfuck.Com.Interfaces;
    using System.Text;

    public class LowLevelCompiler : ICompilerLayer
    {
        public VariableTable VariableTable { get; set; }

        public string Compile(string input)
        {
            var sb = new StringBuilder(input);
            return sb.ToString();
        }
    }
}