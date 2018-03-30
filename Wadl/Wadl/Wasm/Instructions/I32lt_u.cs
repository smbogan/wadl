using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32lt_u : Instruction
    {
        public override void Encode(IEncoder encoder)
        {
            encoder.I32lt_u();
        }
    }
}

