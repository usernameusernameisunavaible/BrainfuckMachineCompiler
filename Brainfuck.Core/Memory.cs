namespace Brainfuck
{
    using System;
    using System.Collections.Generic;

    public class Memory
    {
        private const int blockSize = 2048;
        private readonly List<byte[]> memoryBlocks = new();

        public byte this[int index]
        {
            get
            {
                int blockIndex = index % blockSize;
                int block = (index - blockIndex) / blockSize;

                return memoryBlocks.InBounds(block) ? memoryBlocks[block][blockIndex] : (byte)0;
            }
            set
            {
                int blockIndex = index % blockSize;
                int block = (index - blockIndex) / blockSize;

                while (!memoryBlocks.InBounds(block))
                {
                    memoryBlocks.Add(new byte[blockSize]);
                }

                memoryBlocks[block][blockIndex] = value;
            }
        }

        public void Write(Span<byte> data, int start)
        {
            for (int i = 0; i < data.Length; i++)
            {
                this[i + start] = data[i];
            }
        }
    }
}