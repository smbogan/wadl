using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Wasm
{
    public interface IEncoder
    {
        void Unreachable();
        void Nop();
        void StartBlock(WasmResultTypes resultType);
        void EndBlock();
        void StartLoop(WasmResultTypes resultType);
        void EndLoop();
        void StartIf(WasmResultTypes resultType);
        void EndIf();
        void StartElse();
    }
}
