using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I64extend_s_i32 : Instruction
    {
        public override void Encode(IEncoder encoder)
        {
            encoder.I64extend_s_i32();
        }
    }
}

