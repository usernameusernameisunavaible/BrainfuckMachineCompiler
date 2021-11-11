namespace Brainfuck.Com.Parsers
{
    using Brainfuck.Com.Interfaces;
    using System.Text.RegularExpressions;

    public class PrintParser : IParser
    {
        private static readonly Regex regex1 = new("print ([a-z]*)$", RegexOptions.Compiled);
        private static readonly Regex regex2 = new("print \"(.*)\"", RegexOptions.Compiled);

        public VariableTable VariableTable { get; set; }

        public bool Match(string line, OpStreamWriter stream)
        {
            var match = regex1.Match(line);
            if (match.Success)
            {
                VariableTable.GotoLabel(stream, match.Groups[1].Value);
                stream.Write(OpCode.Output);

                return true;
            }
            match = regex2.Match(line);
            if (match.Success)
            {
                foreach (char chr in match.Groups[1].Value)
                {
                    stream.Write(OpCode.Zero);
                    for (int i = 0; i < chr; i++)
                    {
                        stream.Write(OpCode.IncVal);
                    }

                    stream.Write(OpCode.Output);
                }

                return true;
            }
            return false;
        }
    }
}