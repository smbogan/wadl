using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public abstract class MemoryInstruction : Instruction
    {
        public uint Alignment { get; private set; }

        public uint Offset { get; private set; }

        public MemoryInstruction(uint alignment, uint offset)
        {
            Alignment = alignment;
            Offset = offset;
        }
    }
}
