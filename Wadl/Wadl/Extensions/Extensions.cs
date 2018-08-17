using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wadl.Wasm.Exceptions;

namespace Wadl.Extensions
{
    public static class Extensions
    {
        public static bool OneOf<T>(this T value, params T[] possibilities)
        {
            for(int p = 0; p < possibilities.Length; p++)
            {
                if(possibilities[p].Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static void Expecting<T>(this T value, string message, params T[] possibilities)
        {
            if(!value.OneOf(possibilities))
            {
                throw new UnexpectedException(message, value.ToString(), possibilities.Select(s => s.ToString()).ToArray());
            }
        }
    }
}
