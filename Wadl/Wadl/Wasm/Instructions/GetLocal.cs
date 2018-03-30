using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class GetLocal : Instruction
    {
        public uint LocalIndex { get; private set; }

        public GetLocal(uint localIndex)
        {
            LocalIndex = localIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.GetLocal(LocalIndex);
        }
    }
}
