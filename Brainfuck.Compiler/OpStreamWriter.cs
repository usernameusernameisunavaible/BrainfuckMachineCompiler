namespace Brainfuck.Com
{
    using System.IO;

    public class OpStreamWriter
    {
        private readonly byte[] opbuffer = new byte[1];
        private readonly Stream baseStream;

        public OpStreamWriter(Stream baseStream)
        {
            this.baseStream = baseStream;
        }

        public int Position => (int)baseStream.Position;

        public void Write(OpCode code)
        {
            opbuffer[0] = (byte)code;
            baseStream.Write(opbuffer);
        }
    }
}