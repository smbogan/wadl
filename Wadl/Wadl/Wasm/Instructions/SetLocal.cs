using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class SetLocal : Instruction
    {
        public uint LocalIndex { get; private set; }

        public SetLocal(uint localIndex)
        {
            LocalIndex = localIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.SetLocal(LocalIndex);
        }
    }
}
