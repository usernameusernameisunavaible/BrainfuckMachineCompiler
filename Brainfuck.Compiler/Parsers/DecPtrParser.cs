namespace Brainfuck.Com.Parsers
{
    using Brainfuck.Com.Interfaces;
    using System.Text.RegularExpressions;

    public class DecPtrParser : IParser
    {
        private static readonly Regex regex = new("decptr ([0-9]*)", RegexOptions.Compiled);

        public VariableTable VariableTable { get; set; }

        public bool Match(string line, OpStreamWriter stream)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                var value = int.Parse(match.Groups[1].Value);
                if (value > 0)
                {
                    for (int i = 0; i < value; i++)
                    {
                        stream.Write(OpCode.DecPtr);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}