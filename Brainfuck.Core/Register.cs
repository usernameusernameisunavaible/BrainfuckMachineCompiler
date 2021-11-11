namespace Brainfuck
{
    public class Register
    {
        private byte[] _value;
        private readonly int size;

        public Register(int size)
        {
            this.size = size;
            _value = new byte[size];
        }

        public void Reset()
        {
            _value = new byte[size];
        }

        public byte[] Value
        {
            get => _value;
            set
            {
                for (int i = 0; i < _value.Length; i++)
                {
                    _value[i] = value.Length > i ? value[i] : (byte)0;
                }
            }
        }
    }
}