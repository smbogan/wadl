﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm.Instructions
{
    public class I32Store8 : MemoryInstruction
    {
        public I32Store8(uint alignment, uint offset) 
            : base(alignment, offset)
        {

        }

        public override void Encode(IEncoder encoder)
        {
            encoder.I32Store8(Alignment, Offset);
        }
    }
}
