using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32trunc_s_f64 : Instruction
    {
        public override void Encode(IEncoder encoder)
        {
            encoder.I32trunc_s_f64();
        }
    }
}

