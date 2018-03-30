using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32gt_u : Instruction
    {
        public override void Encode(IEncoder encoder)
        {
            encoder.I32gt_u();
        }
    }
}

