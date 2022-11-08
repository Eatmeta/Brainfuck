using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var openBraceIndex = new Stack<int>();
            var openBracesPositions = new Dictionary<int, int>();
            var closeBracesPositions = new Dictionary<int, int>();
			
            for (var i = 0; i < vm.Instructions.Length; i++)
            {
                switch (vm.Instructions[i])
                {
                    case '[':
                        openBraceIndex.Push(i);
                        break;
                    case ']':
                        openBracesPositions.Add(openBraceIndex.Peek(), i);
                        closeBracesPositions.Add(i, openBraceIndex.Pop());
                        break;
                }
            }
			
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = openBracesPositions[b.InstructionPointer];
            });
			
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = closeBracesPositions[b.InstructionPointer];
            });
        }
    }
}