using System;

namespace BrainfuckMachineCompiler
{
    public interface IAssembler
    {
        /*
         * "setzero" = "[-]"
         * "add {value}" = "+" * value
         * "sub {value}" = "-" * value
         * "cc {value}" = if positive: ">" * value, if negativ: "<" * value //cc = change cell
         * "while" = "["
         * "stop" = "]"
         */

        //for debugging
        public static short lineNr;

        public static string Translate(string bf_assembly)
        {
            ///<summary>
            ///Translates the assemblercode into a brainfuck string.
            /// </summary>
            string temp = string.Empty;
            bool comment = false;
            foreach (char c in bf_assembly)
            {
                if (c == '/')
                {
                    comment = !comment;
                    continue;
                }
                if (!comment)
                {
                    temp += c;
                }
            }
            bf_assembly = temp;

            lineNr = 0;
            string bf_ready = string.Empty;
            foreach (string line in bf_assembly.ToLower().Replace("\r", "\n").Split('\n'))
            {
                bf_ready += Change_Line(line);
                if (bf_ready.Contains("ERROR"))
                {
                    return $"Error in line {lineNr}! {line} cannot be resolved!";
                }
                lineNr++;
            }

            return bf_ready;
        }

        private static string Change_Line(string line)
        {
            ///<summary>
            ///Parses more complex line from assembler to brainfuck. string -> string
            ///</summary>
            int value;
            while (line.Contains("  "))
            {
                line = line.Replace("  ", " ");
            }
            while (line.Contains("\t"))
            {
                line = line.Replace("\t", "");
            }
            string[] parts = line.Split(" ");

            switch (parts[0])
            {
                case "add":
                    return Chr_multiply(Convert.ToInt32(parts[1]), '+');

                case "sub":
                    return Chr_multiply(Convert.ToInt32(parts[1]), '-');

                case "cc":
                    value = Convert.ToInt32(parts[1]);
                    if (value < 0)
                    {
                        return Chr_multiply(Math.Abs(value), '<');
                    }
                    return Chr_multiply(value, '>');

                case "out":
                    return ".";

                case "in":
                    return ",";

                case "setzero":
                    return "[-]";

                case "while":
                    return "[";

                case "stop":
                    return "]";

                case "stopthread":
                    return "x";

                case "pass":
                    return "_";

                case "startthread":
                    return "s";

                case "sleepthread":
                    return "?";

                case "awakethread":
                    return "|";

                case "waitthread":
                    return "&";

                case "endthread":
                    return "#";

                default:
                    if (parts[0] != string.Empty)
                    {
                        return "ERROR";
                    }
                    return string.Empty;
            }
        }

        private static string Chr_multiply(int n, char c)
        {
            ///<summary>
            ///Multiplies chars with ints.
            ///Example: a *3 = aaa
            ///int, char -> string
            /// </summary>
            string result = string.Empty;
            for (int i = 0; i < n; i++)
            {
                result += c;
            }
            return result;
        }

        /*private static bool Bfkonform(string commands)
        {
            Regex rg = new Regex("(\\+|\\-|\\[|\\]|\\<|\\>|\\.|\\,|x)+");
            return rg.Match(commands).Success;
        }
        */
    }
}
