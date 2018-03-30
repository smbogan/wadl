using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class Br : Instruction
    {
        public uint LabelIndex { get; private set; }

        public Br(uint labelIndex)
        {
            LabelIndex = labelIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.Br(LabelIndex);
        }
    }
}
