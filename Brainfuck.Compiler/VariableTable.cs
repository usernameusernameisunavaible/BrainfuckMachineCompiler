namespace Brainfuck.Com
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VariableTable
    {
        public Dictionary<string, int> Variables { get; } = new();

        public int CurrentPointer { get; set; }

        public void Shift(VariableTable table)
        {
            if (table is null) return;
            CurrentPointer = table.CurrentPointer;
        }

        public void Clear()
        {
            CurrentPointer = 0;
            Variables.Clear();
        }

        public void Clear(int position)
        {
            CurrentPointer = position;
            Variables.Clear();
        }

        public void Goto(OpStreamWriter writer, int labelPointer)
        {
            var pointer = CurrentPointer;
            var offset = labelPointer - pointer;

            if (offset > 0)
            {
                for (int i = 0; i < offset; i++)
                {
                    writer.Write(OpCode.IncPtr);
                }
            }
            else if (offset < 0)
            {
                for (int i = 0; i < Math.Abs(offset); i++)
                {
                    writer.Write(OpCode.DecPtr);
                }
            }
        }

        public void AddLabel(OpStreamWriter stream, string name)
        {
            if (Variables.TryGetValue(name, out int labelPointer))
            {
                var pointer = CurrentPointer;
                var offset = labelPointer - pointer;
                CurrentPointer = labelPointer;

                if (offset > 0)
                {
                    for (int i = 0; i < offset; i++)
                    {
                        stream.Write(OpCode.IncPtr);
                    }
                }
                else if (offset < 0)
                {
                    for (int i = 0; i < Math.Abs(offset); i++)
                    {
                        stream.Write(OpCode.DecPtr);
                    }
                }
                stream.Write(OpCode.Zero);
            }
            else
            {
                labelPointer = Variables.Count;
                Variables.Add(name, labelPointer);
                var pointer = CurrentPointer;
                var offset = labelPointer - pointer;
                CurrentPointer = labelPointer;
                CurrentPointer = labelPointer;

                if (offset > 0)
                {
                    for (int i = 0; i < offset; i++)
                    {
                        stream.Write(OpCode.IncPtr);
                    }
                }
                else if (offset < 0)
                {
                    for (int i = 0; i < Math.Abs(offset); i++)
                    {
                        stream.Write(OpCode.DecPtr);
                    }
                }
            }
        }

        public void GotoLabel(OpStreamWriter stream, string name)
        {
            var labelPointer = Variables.First(x => x.Key == name).Value;
            var pointer = CurrentPointer;
            var offset = labelPointer - pointer;
            CurrentPointer = labelPointer;
            if (offset > 0)
            {
                for (int i = 0; i < offset; i++)
                {
                    stream.Write(OpCode.IncPtr);
                }
            }
            else if (offset < 0)
            {
                for (int i = 0; i < Math.Abs(offset); i++)
                {
                    stream.Write(OpCode.DecPtr);
                }
            }
        }
    }
}