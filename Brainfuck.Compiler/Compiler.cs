namespace Brainfuck.Com
{
    using Brainfuck.Com.Interfaces;
    using Brainfuck.Com.Layers;

    public class Compiler : ICompiler
    {
        public Compiler()
        {
            Layers = new ICompilerLayer[]
            {
                new FirstLayerCompiler(),
                new HighLevelLayerCompiler(),
                new LowLevelCompiler()
            }; foreach (var layer in Layers)
            {
                layer.VariableTable = VariableTable;
            }
        }

        public ICompilerLayer[] Layers { get; }

        public VariableTable VariableTable { get; } = new();

        public byte[] Compile(string code)
        {
            VariableTable.Clear();
            var buffer = code;
            foreach (var layer in Layers)
            {
                buffer = layer.Compile(buffer);
            }

            var linker = new Linker(VariableTable);
            return linker.Link(buffer);
        }

        private string CompileInternal(string code)
        {
            VariableTable.Clear();
            var buffer = code;
            foreach (var layer in Layers)
            {
                buffer = layer.Compile(buffer);
            }
            return buffer;
        }

        public byte[] Compile(string[] codes)
        {
            var compilers = new Compiler[codes.Length];
            var results = new string[codes.Length];
            for (int i = 0; i < codes.Length; i++)
            {
                compilers[i] = new();
                results[i] = compilers[i].CompileInternal(codes[i]);
            }

            var linker = new Linker(compilers);
            return linker.Link(results);
        }
    }
}