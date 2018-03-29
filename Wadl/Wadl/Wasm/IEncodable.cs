using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public interface IEncodable
    {
        void Encode(IEncoder encoder);
    }
}
