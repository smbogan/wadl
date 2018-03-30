using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class CallIndirect : Instruction
    {
        public uint TypeIndex { get; private set; }

        public CallIndirect(uint typeIndex)
        {
            TypeIndex = typeIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.CallIndirect(TypeIndex);
        }
    }
}
