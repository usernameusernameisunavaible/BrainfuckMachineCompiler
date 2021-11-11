namespace Brainfuck.Com.Parsers
{
    using Brainfuck.Com.Interfaces;
    using System.Text.RegularExpressions;

    public class ReadParser : IParser
    {
        private readonly Regex regex = new("read (.*)", RegexOptions.Compiled);

        public VariableTable VariableTable { get; set; }

        public bool Match(string line, OpStreamWriter stream)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                if (match.Groups[1].Value != "_")
                    VariableTable.GotoLabel(stream, match.Groups[1].Value);
                stream.Write(OpCode.Input);
                return true;
            }

            return false;
        }
    }
}