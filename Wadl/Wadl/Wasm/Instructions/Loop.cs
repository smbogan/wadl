using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class Loop : Instruction
    {
        public WasmResultTypes ResultType { get; private set; }

        private Instruction[] instructions;

        public IEnumerable<Instruction> Instructions
        {
            get
            {
                foreach (var i in instructions)
                    yield return i;
            }
        }

        public Loop(WasmResultTypes resultType, IEnumerable<Instruction> instructions)
        {
            ResultType = resultType;
            this.instructions = instructions.ToArray();
        }

        public override void Encode(IEncoder encoder)
        {
            encoder.StartLoop(ResultType);

            foreach (var i in Instructions)
            {
                i.Encode(encoder);
            }

            encoder.EndLoop();
        }
    }
}
