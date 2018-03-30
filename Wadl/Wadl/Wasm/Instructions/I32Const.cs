using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32Const : Instruction
    {
        public uint Value { get; private set; }

        public I32Const(int n)
        {
            Value = (uint)n;
        }

        public I32Const(uint n)
        {
            Value = n;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I32Const(Value);
        }
    }
}
