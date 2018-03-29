using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public class TableType : IEncodable
    {
        public ElementType ElementType { get; private set; }

        public Limits Limits { get; private set; }

        public TableType(ElementType elemType, Limits limits)
        {
            ElementType = elemType;
            Limits = limits;
        }

        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }
    }
}
