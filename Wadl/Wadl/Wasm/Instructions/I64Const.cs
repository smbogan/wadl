using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I64Const : Instruction
    {
        public ulong Value { get; private set; }

        public I64Const(ulong n)
        {
            Value = n;
        }

        public I64Const(long n)
        {
            Value = (ulong)n;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I64Const(Value);
        }
    }
}
