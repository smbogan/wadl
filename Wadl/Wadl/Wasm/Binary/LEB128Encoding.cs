using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wadl.Wasm.Exceptions;

namespace Wadl.Wasm.Binary
{
    public static class LEB128Encoding
    {
        public static IEnumerable<byte> EncodeUInt32(uint value)
        {
            do
            {
                uint lob = value & 0x7F;
                value = value >> 7;
                if(value != 0)
                {
                    lob = lob | 0x80; 
                }
                yield return (byte)lob;

            } while (value != 0);
        }

        public static IEnumerable<byte> EncodeInt32(int value)
        {
            bool more = true;
            //bool negative = (value < 0);
            //var size = sizeof(int) * 8;

            while (more)
            {
                byte @byte = ((byte)(value & 0x7F));
                value >>= 7;
                /* the following is only necessary if the implementation of >>= uses a 
                   logical shift rather than an arithmetic shift for a signed left operand */
                //if (negative)
                //    value |= (~0 << (size - 7)); /* sign extend */

                /* sign bit of byte is second high order bit (0x40) */
                if ((value == 0 && ((0x40 & @byte) == 0)) || (value == -1 && ((0x40 & @byte) > 0)))
                    more = false;
                else
                    @byte = (byte)(@byte | 0x80);

                yield return (byte)@byte;
            }
        }

        public static uint DecodeUInt32(Stream stream)
        {
            uint result = 0;
            int shift = 0;
            while (true)
            {
                var read = stream.ReadByte();

                if (read == -1)
                    throw new UnexpectedEndOfProgramException();

                var @byte = (byte)read;
                result |= (uint)(@byte & 0x7F) << shift;
                if ((0x80 & @byte) == 0)
                    break;
                shift += 7;
            }

            return result;
        }

        public static uint DecodeUInt32(IEnumerable<byte> bytes)
        {
            var iterator = bytes.GetEnumerator();

            uint result = 0;
            int shift = 0;
            while (true)
            {
                iterator.MoveNext();
                var @byte = iterator.Current;
                result |= (uint)(@byte & 0x7F) << shift;
                if ((0x80 & @byte) == 0)
                    break;
                shift += 7;
            }

            return result;
        }

        public static int DecodeInt32(Stream stream)
        {
            int result = 0;
            int shift = 0;
            var size = sizeof(int) * 8;
            byte @byte;
            do
            {
                var read = stream.ReadByte();

                if (read == -1)
                    throw new UnexpectedEndOfProgramException();

                @byte = (byte)read;
                result |= ((@byte & 0x7F) << shift);
                shift += 7;
            } while ((0x80 & @byte) != 0);

            /* sign bit of byte is second high order bit (0x40) */
            if ((shift < size) && ((0x40 & @byte) > 0))
                /* sign extend */
                result |= (~0 << shift);

            return result;
        }

        public static int DecodeInt32(IEnumerable<byte> bytes)
        {
            var iterator = bytes.GetEnumerator();

            int result = 0;
            int shift = 0;
            var size = sizeof(int) * 8;
            byte @byte;
            do
            {
                iterator.MoveNext();
                @byte = iterator.Current;
                result |= ((@byte & 0x7F) << shift);
                shift += 7;
            } while ((0x80 & @byte) != 0);

            /* sign bit of byte is second high order bit (0x40) */
            if ((shift < size) && ((0x40 & @byte) > 0))
                /* sign extend */
                result |= (~0 << shift);

            return result;
        }
    }
}
