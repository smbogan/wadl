using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public abstract class Instruction : IEncodable
    {
        public abstract void Encode(IEncoder encoder);
    }
}
