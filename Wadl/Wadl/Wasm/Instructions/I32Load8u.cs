using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32Load8u : MemoryInstruction
    {
        public I32Load8u(uint alignment, uint offset) 
            : base(alignment, offset)
        {

        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I32Load8u(Alignment, Offset);
        }
    }
}
