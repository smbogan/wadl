using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class SetGlobal : Instruction
    {
        public uint GlobalIndex { get; private set; }

        public SetGlobal(uint globalIndex)
        {
            GlobalIndex = globalIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.SetGlobal(GlobalIndex);
        }
    }
}
