using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }

        private readonly Dictionary<char, Action<IVirtualMachine>> _dictionary =
            new Dictionary<char, Action<IVirtualMachine>>();

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            _dictionary.Add(symbol, execute);
        }

        public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                if (_dictionary.ContainsKey(Instructions[InstructionPointer]))
                    _dictionary[Instructions[InstructionPointer]](this);
                InstructionPointer++;
            }
        }
    }
}