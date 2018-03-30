using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class BrTable : Instruction
    {
        private uint[] labelIndices;
        public uint DefaultLabelIndex { get; private set; }

        public IEnumerable<uint> LabelIndices
        {
            get
            {
                foreach(var l in labelIndices)
                {
                    yield return l;
                }
            }
        }

        public BrTable(IEnumerable<uint> labelIndices, uint defaultLabelIndex)
        {
            this.labelIndices = labelIndices.ToArray();
            DefaultLabelIndex = defaultLabelIndex;
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.BrTable(LabelIndices, DefaultLabelIndex);
        }
    }
}
