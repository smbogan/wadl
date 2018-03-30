using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32Load16s : MemoryInstruction
    {
        public I32Load16s(uint alignment, uint offset) 
            : base(alignment, offset)
        {

        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I32Load16s(Alignment, Offset);
        }
    }
}
