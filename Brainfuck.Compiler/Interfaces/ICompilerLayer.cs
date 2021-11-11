namespace Brainfuck.Com.Interfaces
{
    public interface ICompilerLayer
    {
        public VariableTable VariableTable { get; set; }

        public string Compile(string input);
    }
}