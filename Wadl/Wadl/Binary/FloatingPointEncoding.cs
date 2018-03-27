using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Binary
{
    public static class FloatingPointEncoding
    {
        

        public static IEnumerable<byte> EncodeF32(float value)
        {
            byte[] EncodeF32AsBytes(float value)
            {
                byte[] bytes = new byte[sizeof(float)];

                unsafe
                {
                    byte* b = (byte*)&value;

                    for (int i = 0; i < sizeof(float); i++)
                    {
                        bytes[i] = *b;
                        b++;
                    }
                }

                return bytes;
            }

        var bytesx = EncodeF32AsBytes(value);
            foreach (var b in bytesx)
                yield return b;
        }
    }
}
