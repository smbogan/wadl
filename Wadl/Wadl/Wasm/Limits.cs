using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public class Limits : IEncodable
    {
        public int Min { get; private set; }
        public int? Max { get; private set; }

        public Limits(int min, int? max = null)
        {
            Min = min;
            Max = max;
        }

        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }
    }
}
