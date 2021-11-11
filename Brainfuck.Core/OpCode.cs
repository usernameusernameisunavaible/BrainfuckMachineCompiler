namespace Brainfuck
{
    public enum OpCode : byte
    {
        Nop = 0x00,
        IncPtr = 0x01,
        DecPtr = 0x02,
        IncVal = 0x03,
        DecVal = 0x04,
        Output = 0x05,
        Input = 0x06,
        Zero = 0x07,
        WhileBegin = 0x08,
        WhileEnd = 0x09,
        StartThread = 0x0A,
        StopThread = 0x0B,
        SleepThread = 0x0C,
        AwakeThread = 0x0D,
        WaitThread = 0x0E,
        EndThread = 0x0F,
        LoadIP = 0x10,
    }
}