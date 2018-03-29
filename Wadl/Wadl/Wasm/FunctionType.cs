using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public class FunctionType : IEncodable
    {
        WasmValueTypes[] parameters;
        WasmValueTypes[] results;

        public IEnumerable<WasmValueTypes> Parameters
        {
            get
            {
                foreach (var p in parameters)
                    yield return p;
            }
        }

        public IEnumerable<WasmValueTypes> Results
        {
            get
            {
                foreach (var r in results)
                {
                    yield return r;
                }
            }
        }

        public FunctionType(IEnumerable<WasmValueTypes> parameters, IEnumerable<WasmValueTypes> results)
        {
            this.parameters = parameters.ToArray();
            this.results = results.ToArray();
        }

        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }
    }
}
