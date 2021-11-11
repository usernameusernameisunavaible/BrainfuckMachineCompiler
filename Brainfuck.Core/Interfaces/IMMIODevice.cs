namespace Brainfuck.Interfaces
{
    public interface IMMIODevice
    {
        Memory Memory { get; set; }

        MemoryRegion Region { get; set; }

        public void Tick();
    }
}