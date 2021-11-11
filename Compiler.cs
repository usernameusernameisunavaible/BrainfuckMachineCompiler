using System.Collections.Generic;

namespace BrainfuckMachineCompiler
{
    public enum Opcode
    {
        Nop = 0x00,   //_
        IncPtr = 0x01,   //>
        DecPtr = 0x02,   //<
        IncVal = 0x03,   //+
        DecVal = 0x04,   //-
        Output = 0x05,   //.
        Input = 0x06,   //,
        WhileBegin = 0x07,   //[
        WhileEnd = 0x08,   //]
        StartThread = 0x09,   //s
        StopThread = 0x0A,   //x
        SleepThread = 0x0B,   //?
        AwakteThread = 0x0C,   //|
        WaitThread = 0x0D,   //&
        EndThread = 0x0E    //#
    }

    class Compiler
    {
        public static byte[] Compile(string brainfuck)
        {
            List<byte> byte_code = new();

            foreach (char c in brainfuck)
            {
                switch (c)
                {
                    case '_':
                        byte_code.Add((byte)Opcode.Nop);
                        break;

                    case '>':
                        byte_code.Add((byte)Opcode.IncPtr);
                        break;

                    case '<':
                        byte_code.Add((byte)Opcode.DecPtr);
                        break;

                    case '+':
                        byte_code.Add((byte)Opcode.IncVal);
                        break;

                    case '-':
                        byte_code.Add((byte)Opcode.DecVal);
                        break;

                    case '.':
                        byte_code.Add((byte)Opcode.Output);
                        break;

                    case ',':
                        byte_code.Add((byte)Opcode.Input);
                        break;

                    case '[':
                        byte_code.Add((byte)Opcode.WhileBegin);
                        break;

                    case ']':
                        byte_code.Add((byte)Opcode.WhileEnd);
                        break;

                    case 's':
                        byte_code.Add((byte)Opcode.StartThread);
                        break;

                    case 'x':
                        byte_code.Add((byte)Opcode.StopThread);
                        break;

                    case '?':
                        byte_code.Add((byte)Opcode.SleepThread);
                        break;

                    case '|':
                        byte_code.Add((byte)Opcode.AwakteThread);
                        break;

                    case '&':
                        byte_code.Add((byte)Opcode.WaitThread);
                        break;

                    case '#':
                        byte_code.Add((byte)Opcode.EndThread);
                        break;

                    default:
                        return System.Array.Empty<byte>();
                }
            }

            return byte_code.ToArray();
        }
    }
}
