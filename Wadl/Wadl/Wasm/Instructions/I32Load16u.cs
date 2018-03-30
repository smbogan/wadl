using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32Load16u : MemoryInstruction
    {
        public I32Load16u(uint alignment, uint offset) 
            : base(alignment, offset)
        {
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I32Load16u(Alignment, Offset);
        }
    }
}
