using Brainfuck.Com.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Brainfuck.Com
{
    public class Linker
    {
        public const int MagicNumber = (int)0xFU;

        public Linker(VariableTable variableTable)
        {
            VariableTables = new VariableTable[] { variableTable };
        }

        public Linker(Compiler[] compilers)
        {
            VariableTables = new VariableTable[compilers.Length];
            for (int i = 0; i < compilers.Length; i++)
            {
                VariableTables[i] = compilers[i].VariableTable;
            }
        }

        public VariableTable[] VariableTables { get; }

        public byte[] Link(string[] input)
        {
            throw new NotImplementedException("");
            /*
            var output = new MemoryStream();
            IParser[] parsers = Assembly.GetEntryAssembly().GetTypes().ToList().Where(x => x.GetInterfaces().Any(x => x == typeof(IParser))).ToList().ConvertAll(x => (IParser)Activator.CreateInstance(x)).ToArray();
            output.Write(BitConverter.GetBytes(MagicNumber));
            output.Write(BitConverter.GetBytes(input.Length));
            var writer = new OpStreamWriter(output);
            VariableTable tableBefore = null;
            for (int i = 0; i < input.Length; i++)
            {
                foreach (var parser in parsers)
                {
                    parser.VariableTable = VariableTables[i];
                }
            }

            writer.Write(OpCode.EndThread);
            var result = output.ToArray();
            output.Dispose();
            return result;
            */
        }

        public byte[] Link(string input)
        {
            var text = input;
            var lines = text.Split(Environment.NewLine);

            // Von jedem Typ mit dem Interface IParser wird eine Instanz erstellt und in eine Array gespeichert.
            IParser[] parsers = Assembly.GetEntryAssembly().GetTypes().ToList().Where(x => x.GetInterfaces().Any(x => x == typeof(IParser))).ToList().ConvertAll(x => (IParser)Activator.CreateInstance(x)).ToArray();
            VariableTables[0].CurrentPointer = 0;
            var output = new MemoryStream();
            output.Write(BitConverter.GetBytes(MagicNumber));
            output.Write(BitConverter.GetBytes(input.Length));
            var writer = new OpStreamWriter(output);
            foreach (var parser in parsers)
            {
                parser.VariableTable = VariableTables[0];
            }
            foreach (string line in lines)
            {
                var success = false;
                foreach (var parser in parsers)
                {
                    if (parser.Match(line, writer))
                    {
                        success = true;
                    }
                }

                if (!success)
                {
                    foreach (char chr in line)
                    {
                        writer.Write(GetCode(chr));
                    }
                }
            }

            writer.Write(OpCode.EndThread);
            var result = output.ToArray();
            output.Dispose();
            return result;
        }

        private static OpCode GetCode(char chr)
        {
            return chr switch
            {
                '>' => OpCode.IncPtr,
                '<' => OpCode.DecPtr,
                '+' => OpCode.IncVal,
                '-' => OpCode.DecVal,
                '.' => OpCode.Output,
                ',' => OpCode.Input,
                '*' => OpCode.Zero,
                '[' => OpCode.WhileBegin,
                ']' => OpCode.WhileEnd,
                'x' => OpCode.EndThread,
                's' => OpCode.StartThread,
                '#' => OpCode.StopThread,
                '?' => OpCode.SleepThread,
                '|' => OpCode.AwakeThread,
                '&' => OpCode.WaitThread,
                _ => OpCode.Nop,
            };
        }
    }
}