namespace Brainfuck
{
    using System.Collections.Generic;

    public class Processor
    {
        public Memory Memory { get; } = new();

        public List<ProcessorThread> Threads { get; } = new();
    }
}