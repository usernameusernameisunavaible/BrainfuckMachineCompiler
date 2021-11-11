namespace Brainfuck.Com.Layers
{
    using Brainfuck.Com.Interfaces;
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Der First Layer Compiler Trimt den Text und entfernt Kommentare.
    /// (Vorbearbeitung)
    /// </summary>
    public class FirstLayerCompiler : ICompilerLayer
    {
        private readonly Regex commentRegex = new("//(?<=//)(.*)(?=)", RegexOptions.Compiled);

        public VariableTable VariableTable { get; set; }

        public string Compile(string input)
        {
            var sb = new StringBuilder();
            foreach (var line in input.Split(Environment.NewLine))
            {
                sb.AppendLine(commentRegex.Replace(line, string.Empty).Trim());
            }
            return sb.ToString();
        }
    }
}