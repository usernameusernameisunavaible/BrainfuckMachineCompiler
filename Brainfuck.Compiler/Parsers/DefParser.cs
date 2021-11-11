namespace Brainfuck.Com.Parsers
{
    using Brainfuck.Com.Interfaces;
    using System;
    using System.Text.RegularExpressions;

    public class DefParser : IParser
    {
        private static readonly Regex regex1 = new("([a-z]*) = ([0-9]*)$", RegexOptions.Compiled);
        private static readonly Regex regex2 = new("([a-z]*) = '(.)'$", RegexOptions.Compiled);

        public VariableTable VariableTable { get; set; }

        public bool Match(string line, OpStreamWriter stream)
        {
            var match = regex1.Match(line);
            if (match.Success)
            {
                VariableTable.AddLabel(stream, match.Groups[1].Value);
                Define(stream, byte.Parse(match.Groups[2].Value));
                return true;
            }
            match = regex2.Match(line);
            if (match.Success)
            {
                VariableTable.AddLabel(stream, match.Groups[1].Value);
                Define(stream, (byte)match.Groups[2].Value[0]);
                return true;
            }
            return false;
        }

        private static void Define(OpStreamWriter stream, byte value)
        {
            if (value > 0)
            {
                for (int i = 0; i < value; i++)
                {
                    stream.Write(OpCode.IncVal);
                }
            }
            else if (value < 0)
            {
                for (int i = 0; i < Math.Abs(value); i++)
                {
                    stream.Write(OpCode.DecVal);
                }
            }
        }
    }
}