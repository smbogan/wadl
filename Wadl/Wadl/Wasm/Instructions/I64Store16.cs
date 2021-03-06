﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I64Store16 : MemoryInstruction
    {
        public I64Store16(uint alignment, uint offset) 
            : base(alignment, offset)
        {

        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I64Store16(Alignment, Offset);
        }
    }
}
