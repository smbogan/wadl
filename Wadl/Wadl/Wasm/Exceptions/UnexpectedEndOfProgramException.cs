using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Exceptions
{

    [Serializable]
    public class UnexpectedEndOfProgramException : Exception
    {
        public UnexpectedEndOfProgramException()
        {

        }

        protected UnexpectedEndOfProgramException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context)
        {

        }
    }
}
