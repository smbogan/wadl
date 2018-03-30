using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class F32Store : MemoryInstruction
    {
        public F32Store(uint alignment, uint offset) 
            : base(alignment, offset)
        {

        }

        public override void Encode(IEncoder encoder)
        {
            encoder.F32Store(Alignment, Offset);
        }
    }
}
