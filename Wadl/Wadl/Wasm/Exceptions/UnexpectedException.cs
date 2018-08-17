using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Exceptions
{

    [Serializable]
    public class UnexpectedException : Exception
    {
        private static string CreateMessage(string message, string value, string[] possibilities)
        {
            return $"{message}.  Found {value}.  Expected one of: {string.Join(".", possibilities)}";
        }

        public UnexpectedException(string message, string value, string[] possibilities)
            : base(CreateMessage(message, value, possibilities))
        {

        }
        
        protected UnexpectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
