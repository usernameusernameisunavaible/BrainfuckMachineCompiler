namespace Brainfuck.Com.Interfaces
{
    public interface IParser
    {
        public VariableTable VariableTable { get; set; }

        public bool Match(string line, OpStreamWriter stream);
    }
}