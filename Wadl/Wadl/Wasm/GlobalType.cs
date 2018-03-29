using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public class GlobalType : IEncodable
    {
        public Mutability Mutability { get; private set; }

        public WasmValueTypes ValueType { get; private set; }

        public GlobalType(Mutability mutability, WasmValueTypes valueType)
        {
            Mutability = mutability;
            ValueType = valueType;
        }

        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }
    }
}
