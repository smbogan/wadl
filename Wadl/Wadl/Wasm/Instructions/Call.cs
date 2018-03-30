using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class Call : Instruction
    {
        public uint FunctionIndex { get; private set; }

        public Call(uint functionIndex)
        {
            FunctionIndex = functionIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.Call(FunctionIndex);
        }
    }
}
