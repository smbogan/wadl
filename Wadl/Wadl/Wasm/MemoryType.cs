using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public class MemoryType : IEncodable
    {
        public Limits Limits { get; private set; }

        public MemoryType(Limits limits)
        {
            Limits = limits;
        }

        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }
    }
}
