using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo (IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            unchecked
            {
                vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer]++; });
                vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer]--; });
            }
            vm.RegisterCommand('.', b => { write((char) b.Memory[b.MemoryPointer]); });
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte) read(); });

            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1)
                    b.MemoryPointer = 0;
                else
                    b.MemoryPointer++;
            });
            
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length - 1;
                else
                    b.MemoryPointer--;
            });

            var addedSymbols = new List<char>();
            for (var i = 0; i < vm.Instructions.Length; i++)
            {
                if (!char.IsLetterOrDigit(vm.Instructions[i]) || addedSymbols.Contains(vm.Instructions[i])) 
                    continue;
                addedSymbols.Add(vm.Instructions[i]);
                var i1 = i;
                vm.RegisterCommand(vm.Instructions[i1], b =>
                {
                    b.Memory[b.MemoryPointer] = (byte) vm.Instructions[i1];
                });
            }
        }
    }
}