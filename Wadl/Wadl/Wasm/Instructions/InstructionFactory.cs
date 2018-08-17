using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wadl.Extensions;
using Wadl.Wasm.Binary;
using Wadl.Wasm.Exceptions;

namespace Wadl.Wasm.Instructions
{
    public class InstructionFactory
    {
        
        
        public static WaOpCode? DecodeInstruction(Stream stream, List<Instruction> output)
        {
            WasmResultTypes ReadResultType()
            {
                var resultTypeRead = stream.ReadByte();

                if (resultTypeRead == -1)
                    throw new UnexpectedEndOfProgramException();

                WasmResultTypes wasmReturnType = (WasmResultTypes)resultTypeRead;
                wasmReturnType.Expecting("Unexpected return type", WasmResultTypes.None, WasmResultTypes.f32, WasmResultTypes.f64, WasmResultTypes.i32, WasmResultTypes.i64);

                return wasmReturnType;
            }

            var read = stream.ReadByte();

            if (read == -1)
                return null;

            while (read != -1)
            {
                var opcode = (WaOpCode)read;

                switch (opcode)
                {
                    case WaOpCode.unreachable:
                        output.Add(new Unreachable());
                        return opcode;
                    case WaOpCode.nop:
                        output.Add(new Nop());
                        return opcode;
                    case WaOpCode.block:
                        {
                            WasmResultTypes returnType = ReadResultType();

                            List<Instruction> blockInstructions = new List<Instruction>();
                            var blockEnd = DecodeInstruction(stream, blockInstructions);

                            if (blockEnd != WaOpCode.end)
                                throw new UnexpectedException("Expecting end of instruction sequence instruction (0x0B)", blockEnd.ToString(), new string[] { WaOpCode.end.ToString() });

                            output.Add(new Block(returnType, blockInstructions));
                        }
                        break;
                    case WaOpCode.loop:
                        {
                            WasmResultTypes returnType = ReadResultType();

                            List<Instruction> loopInstructions = new List<Instruction>();
                            var blockEnd = DecodeInstruction(stream, loopInstructions);

                            if (blockEnd != WaOpCode.end)
                                throw new UnexpectedException("Expecting end of instruction sequence op code (0x0B)", blockEnd.ToString(), new string[] { WaOpCode.end.ToString() });

                            output.Add(new Loop(returnType, loopInstructions));
                        }
                        break;
                    case WaOpCode.@if:
                        {
                            WasmResultTypes returnType = ReadResultType();

                            List<Instruction> trueCase = new List<Instruction>();
                            List<Instruction> falseCase = new List<Instruction>();
                            var trueCaseEnd = DecodeInstruction(stream, trueCase);

                            trueCaseEnd.Expecting("Expecting either the else op code (0x05) or the end of instruction sequence op code (0x0B)", WaOpCode.@else, WaOpCode.end);

                            if(trueCaseEnd == WaOpCode.@else)
                            {
                                var falseCaseEnd = DecodeInstruction(stream, falseCase);

                                falseCaseEnd.Expecting("Expecting end of instruction sequence op code (0x0B)", WaOpCode.end);
                            }

                            output.Add(new If(returnType, trueCase, falseCase));
                        }
                        break;
                    case WaOpCode.@else:
                        return opcode;
                    case WaOpCode.end:
                        return opcode;
                    case WaOpCode.br:
                        {
                            var labelidx = LEB128Encoding.DecodeUInt32(stream);
                            output.Add(new Br(labelidx));
                        }
                        break;
                    case WaOpCode.br_if:
                        {
                            var labelidx = LEB128Encoding.DecodeUInt32(stream);
                            output.Add(new BrIf(labelidx));
                        }
                        break;
                    case WaOpCode.br_table:
                        {
                            uint vectorCount = LEB128Encoding.DecodeUInt32(stream);

                            List<uint> labels = new List<uint>();

                            for(int v = 0; v < vectorCount; v++)
                            {
                                uint label = LEB128Encoding.DecodeUInt32(stream);
                                labels.Add(label);
                            }

                            uint defaultLabel = LEB128Encoding.DecodeUInt32(stream);

                            output.Add(new BrTable(labels, defaultLabel));
                        }
                        break;
                    case WaOpCode.@return:
                        {
                            output.Add(new Return());
                        }
                        break;
                    case WaOpCode.call:
                        {
                            uint functionIdx = LEB128Encoding.DecodeUInt32(stream);

                            output.Add(new Call(functionIdx));
                        }
                        break;
                    case WaOpCode.call_indirect:
                        {
                            uint typeIdx = LEB128Encoding.DecodeUInt32(stream);

                            output.Add(new CallIndirect(typeIdx));
                        }
                        break;
                    case WaOpCode.drop:
                        {
                            output.Add(new Drop());
                        }
                        break;
                    case WaOpCode.select:
                        {
                            output.Add(new Select());
                        }
                        break;
                    case WaOpCode.get_local:
                        {
                            output.Add(new GetLocal(LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.set_local:
                        {
                            output.Add(new SetLocal(LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.tee_local:
                        {
                            output.Add(new TeeLocal(LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.get_global:
                        {
                            output.Add(new GetGlobal(LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.set_global:
                        {
                            output.Add(new SetGlobal(LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32load:
                        {
                            output.Add(new I32Load(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load:
                        {
                            output.Add(new I64Load(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.f32load:
                        {
                            output.Add(new F32Load(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.f64load:
                        {
                            output.Add(new F64Load(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32load8_s:
                        {
                            output.Add(new I32Load8s(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32load8_u:
                        {
                            output.Add(new I32Load8u(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32load16_s:
                        {
                            output.Add(new I32Load16s(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32load16_u:
                        {
                            output.Add(new I32Load16u(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load8_s:
                        {
                            output.Add(new I64Load8s(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load8_u:
                        {
                            output.Add(new I64Load8u(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load16_s:
                        {
                            output.Add(new I64Load16s(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load16_u:
                        {
                            output.Add(new I64Load16u(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load32_s:
                        {
                            output.Add(new I64Load32s(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64load32_u:
                        {
                            output.Add(new I64Load32u(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32store:
                        {
                            output.Add(new I32Store(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64store:
                        {
                            output.Add(new I64Store(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.f32store:
                        {
                            output.Add(new F32Store(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.f64store:
                        {
                            output.Add(new F64Store(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32store8:
                        {
                            output.Add(new I32Store8(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i32store16:
                        {
                            output.Add(new I32Store16(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64store8:
                        {
                            output.Add(new I64Store8(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64store16:
                        {
                            output.Add(new I64Store16(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.i64store32:
                        {
                            output.Add(new I64Store32(LEB128Encoding.DecodeUInt32(stream), LEB128Encoding.DecodeUInt32(stream)));
                        }
                        break;
                    case WaOpCode.current_memory:
                        {
                            output.Add(new CurrentMemory());
                        }
                        break;
                    case WaOpCode.grow_memory:
                        {
                            output.Add(new GrowMemory());
                        }
                        break;
                    case WaOpCode.i32const:
                        {

                        }
                        break;
                    case WaOpCode.i64const:
                        break;
                    case WaOpCode.f32const:
                        break;
                    case WaOpCode.f64const:
                        break;
                    case WaOpCode.i32eqz:
                        break;
                    case WaOpCode.i32eq:
                        break;
                    case WaOpCode.i32ne:
                        break;
                    case WaOpCode.i32lt_s:
                        break;
                    case WaOpCode.i32lt_u:
                        break;
                    case WaOpCode.i32gt_s:
                        break;
                    case WaOpCode.i32gt_u:
                        break;
                    case WaOpCode.i32le_s:
                        break;
                    case WaOpCode.i32le_u:
                        break;
                    case WaOpCode.i32ge_s:
                        break;
                    case WaOpCode.i32ge_u:
                        break;
                    case WaOpCode.i64eqz:
                        break;
                    case WaOpCode.i64eq:
                        break;
                    case WaOpCode.i64ne:
                        break;
                    case WaOpCode.i64lt_s:
                        break;
                    case WaOpCode.i64lt_u:
                        break;
                    case WaOpCode.i64gt_s:
                        break;
                    case WaOpCode.i64gt_u:
                        break;
                    case WaOpCode.i64le_s:
                        break;
                    case WaOpCode.i64le_u:
                        break;
                    case WaOpCode.i64ge_s:
                        break;
                    case WaOpCode.i64ge_u:
                        break;
                    case WaOpCode.f32eq:
                        break;
                    case WaOpCode.f32ne:
                        break;
                    case WaOpCode.f32lt:
                        break;
                    case WaOpCode.f32gt:
                        break;
                    case WaOpCode.f32le:
                        break;
                    case WaOpCode.f32ge:
                        break;
                    case WaOpCode.f64eq:
                        break;
                    case WaOpCode.f64ne:
                        break;
                    case WaOpCode.f64lt:
                        break;
                    case WaOpCode.f64gt:
                        break;
                    case WaOpCode.f64le:
                        break;
                    case WaOpCode.f64ge:
                        break;
                    case WaOpCode.i32clz:
                        break;
                    case WaOpCode.i32ctz:
                        break;
                    case WaOpCode.i32popcnt:
                        break;
                    case WaOpCode.i32add:
                        break;
                    case WaOpCode.i32sub:
                        break;
                    case WaOpCode.i32mul:
                        break;
                    case WaOpCode.i32div_s:
                        break;
                    case WaOpCode.i32div_u:
                        break;
                    case WaOpCode.i32rem_s:
                        break;
                    case WaOpCode.i32rem_u:
                        break;
                    case WaOpCode.i32and:
                        break;
                    case WaOpCode.i32or:
                        break;
                    case WaOpCode.i32xor:
                        break;
                    case WaOpCode.i32shl:
                        break;
                    case WaOpCode.i32shr_s:
                        break;
                    case WaOpCode.i32shr_u:
                        break;
                    case WaOpCode.i32rotl:
                        break;
                    case WaOpCode.i32rotr:
                        break;
                    case WaOpCode.i64clz:
                        break;
                    case WaOpCode.i64ctz:
                        break;
                    case WaOpCode.i64popcnt:
                        break;
                    case WaOpCode.i64add:
                        break;
                    case WaOpCode.i64sub:
                        break;
                    case WaOpCode.i64mul:
                        break;
                    case WaOpCode.i64div_s:
                        break;
                    case WaOpCode.i64div_u:
                        break;
                    case WaOpCode.i64rem_s:
                        break;
                    case WaOpCode.i64rem_u:
                        break;
                    case WaOpCode.i64and:
                        break;
                    case WaOpCode.i64or:
                        break;
                    case WaOpCode.i64xor:
                        break;
                    case WaOpCode.i64shl:
                        break;
                    case WaOpCode.i64shr_s:
                        break;
                    case WaOpCode.i64shr_u:
                        break;
                    case WaOpCode.i64rotl:
                        break;
                    case WaOpCode.i64rotr:
                        break;
                    case WaOpCode.f32abs:
                        break;
                    case WaOpCode.f32neg:
                        break;
                    case WaOpCode.f32ceil:
                        break;
                    case WaOpCode.f32floor:
                        break;
                    case WaOpCode.f32trunc:
                        break;
                    case WaOpCode.f32nearest:
                        break;
                    case WaOpCode.f32sqrt:
                        break;
                    case WaOpCode.f32add:
                        break;
                    case WaOpCode.f32sub:
                        break;
                    case WaOpCode.f32mul:
                        break;
                    case WaOpCode.f32div:
                        break;
                    case WaOpCode.f32min:
                        break;
                    case WaOpCode.f32max:
                        break;
                    case WaOpCode.f32copysign:
                        break;
                    case WaOpCode.f64abs:
                        break;
                    case WaOpCode.f64neg:
                        break;
                    case WaOpCode.f64ceil:
                        break;
                    case WaOpCode.f64floor:
                        break;
                    case WaOpCode.f64trunc:
                        break;
                    case WaOpCode.f64nearest:
                        break;
                    case WaOpCode.f64sqrt:
                        break;
                    case WaOpCode.f64add:
                        break;
                    case WaOpCode.f64sub:
                        break;
                    case WaOpCode.f64mul:
                        break;
                    case WaOpCode.f64div:
                        break;
                    case WaOpCode.f64min:
                        break;
                    case WaOpCode.f64max:
                        break;
                    case WaOpCode.f64copysign:
                        break;
                    case WaOpCode.i32wrap_i64:
                        break;
                    case WaOpCode.i32trunc_s_f32:
                        break;
                    case WaOpCode.i32trunc_u_f32:
                        break;
                    case WaOpCode.i32trunc_s_f64:
                        break;
                    case WaOpCode.i32trunc_u_f64:
                        break;
                    case WaOpCode.i64extend_s_i32:
                        break;
                    case WaOpCode.i64extend_u_i32:
                        break;
                    case WaOpCode.i64trunc_s_f32:
                        break;
                    case WaOpCode.i64trunc_u_f32:
                        break;
                    case WaOpCode.i64trunc_s_f64:
                        break;
                    case WaOpCode.i64trunc_u_f64:
                        break;
                    case WaOpCode.f32convert_s_i32:
                        break;
                    case WaOpCode.f32convert_u_i32:
                        break;
                    case WaOpCode.f32convert_s_i64:
                        break;
                    case WaOpCode.f32convert_u_i64:
                        break;
                    case WaOpCode.f32demote_f64:
                        break;
                    case WaOpCode.f64convert_s_i32:
                        break;
                    case WaOpCode.f64convert_u_i32:
                        break;
                    case WaOpCode.f64convert_s_i64:
                        break;
                    case WaOpCode.f64convert_u_i64:
                        break;
                    case WaOpCode.f64promote_f32:
                        break;
                    case WaOpCode.i32reinterpret_f32:
                        break;
                    case WaOpCode.i64reinterpret_f64:
                        break;
                    case WaOpCode.f32reinterpret_i32:
                        break;
                    case WaOpCode.f64reinterpret_i64:
                        break;
                    default:
                        break;
                }

                read = stream.ReadByte();
            }

            return null;
        }


        

        public static List<Instruction> LoadInstructions(Stream stream)
        {
            var instructions = new List<Instruction>();

            using (var buffer = new BufferedStream(stream))
            {
                DecodeInstruction(stream, instructions);
            }

            return instructions;
        }


    }
}
