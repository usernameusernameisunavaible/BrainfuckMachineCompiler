namespace Brainfuck
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    public class ProcessorThread
    {
        private Thread thread;
        private bool isRunning = true;
        private bool sleeping = false;
        private readonly Stack<int> loopStack = new();

        public ProcessorThread(Processor processor)
        {
            Processor = processor;
            Memory = processor.Memory;
        }

        public int Pointer { get; set; }

        public int ExecutionPointer { get; set; }

        public Memory Memory { get; }

        public Processor Processor { get; }

        public void Start()
        {
            thread = new(ThreadExecutionVoid);
            thread.Start();
        }

        public void Stop()
        {
            isRunning = false;
            while (thread.IsAlive) Thread.Sleep(1);
        }

        public void Sleep()
        {
            sleeping = true;
        }

        public void Awake()
        {
            sleeping = false;
        }

        private OpCode GetCode()
        {
            return (OpCode)Memory[ExecutionPointer];
        }

        private void ThreadExecutionVoid()
        {
            while (isRunning)
            {
                while (sleeping & isRunning) Thread.Sleep(1);
                if (!isRunning) return;
                var op = GetCode();
                switch (op)
                {
                    case OpCode.Nop:
                        ExecutionPointer++;
                        break;

                    case OpCode.IncPtr:
                        ExecutionPointer++;
                        Pointer++;
                        break;

                    case OpCode.DecPtr:
                        ExecutionPointer++;
                        Pointer--;
                        break;

                    case OpCode.IncVal:
                        ExecutionPointer++;
                        Memory[Pointer]++;
                        break;

                    case OpCode.DecVal:
                        ExecutionPointer++;
                        Memory[Pointer]--;
                        break;

                    case OpCode.Output:
                        ExecutionPointer++;
                        Console.Write(Encoding.UTF8.GetString(new byte[] { Memory[Pointer] }));
                        break;

                    case OpCode.Input:
                        ExecutionPointer++;
                        Memory[Pointer] = (byte)Console.Read();
                        break;

                    case OpCode.Zero:
                        ExecutionPointer++;
                        Memory[Pointer] = 0;
                        break;

                    case OpCode.WhileBegin:
                        if (Memory[Pointer] != 0)
                        {
                            loopStack.Push(ExecutionPointer);
                            ExecutionPointer++;
                        }
                        else
                        {
                            while (GetCode() != OpCode.WhileEnd) ExecutionPointer++;
                        }
                        break;

                    case OpCode.WhileEnd:
                        if (Memory[Pointer] == 0)
                        {
                            ExecutionPointer++;
                        }
                        else
                        {
                            ExecutionPointer = loopStack.Pop();
                        }
                        break;

                    case OpCode.StartThread:
                        {
                            var thread = new ProcessorThread(Processor) { ExecutionPointer = Memory[Pointer] };
                            Processor.Threads.Add(thread);
                            thread.Start();
                            ExecutionPointer++;
                        }
                        break;

                    case OpCode.StopThread:
                        {
                            var thread = Processor.Threads[Memory[Pointer]];
                            thread.Stop();
                            ExecutionPointer++;
                        }
                        break;

                    case OpCode.SleepThread:
                        {
                            var thread = Processor.Threads[Memory[Pointer]];
                            thread.Sleep();
                            ExecutionPointer++;
                        }
                        break;

                    case OpCode.AwakeThread:
                        {
                            var thread = Processor.Threads[Memory[Pointer]];
                            thread.Awake();
                            ExecutionPointer++;
                        }
                        break;

                    case OpCode.WaitThread:
                        {
                            Sleep();
                            ExecutionPointer++;
                        }
                        break;

                    case OpCode.EndThread:
                        {
                            isRunning = false;
                            ExecutionPointer++;
                        }
                        break;
                }
            }
        }
    }
}