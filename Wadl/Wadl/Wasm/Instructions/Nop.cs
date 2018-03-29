using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class Nop : Instruction
    {
        public override void Encode(IEncoder encoder)
        {
            encoder.Nop();
        }
    }
}
