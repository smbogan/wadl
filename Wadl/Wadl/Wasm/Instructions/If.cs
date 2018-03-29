using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class If : Instruction
    {
        public WasmResultTypes ResultType { get; private set; }

        private Instruction[] trueInstructions;

        public IEnumerable<Instruction> TrueInstructions
        {
            get
            {
                foreach (var i in trueInstructions)
                    yield return i;
            }
        }

        private Instruction[] falseInstructions;

        public IEnumerable<Instruction> FalseInstructions
        {
            get
            {
                foreach (var i in falseInstructions)
                    yield return i;
            }
        }

        public If(WasmResultTypes resultType, IEnumerable<Instruction> trueInstructions, IEnumerable<Instruction> falseInstruction)
        {
            ResultType = resultType;
            this.trueInstructions = this.trueInstructions.ToArray();
            this.falseInstructions = falseInstruction.ToArray();
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.StartIf(ResultType);

            foreach (var i in TrueInstructions)
            {
                i.Encode(encoder);
            }

            if(FalseInstructions.Any())
            {
                encoder.StartElse();

                foreach(var i in FalseInstructions)
                {
                    i.Encode(encoder);
                }
            }

            encoder.EndIf();
        }
    }
}
