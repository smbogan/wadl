using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class F32Const : Instruction
    {
        public float Value { get; private set; }

        public F32Const(float z)
        {
            Value = z;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.F32Const(Value);
        }
    }
}
