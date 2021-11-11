namespace Brainfuck.Com.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICompiler
    {
        public static ICompilerLayer[] Layers { get; }

        public byte[] Compile(string code);

        public byte[] Compile(string[] codes);
    }
}