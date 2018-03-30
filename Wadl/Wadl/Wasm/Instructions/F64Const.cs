using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class F64Const : Instruction
    {
        public double Value { get; private set; }

        public F64Const(double value)
        {
            Value = value;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.F64Const(Value);
        }
    }
}
