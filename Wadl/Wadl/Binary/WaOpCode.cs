﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wadl.Binary
{
    public enum WaOpCode : byte
    {
        unreachable = 0x00,
        nop = 0x01,
        block = 0x02,
        loop = 0x03,
        @if = 0x04,
        @else = 0x05,
        //Reserved: 0x06 - 0x0A
        @end = 0x0B,
        br = 0x0C,
        br_if = 0x0D,
        br_table = 0x0E,
        @return = 0x0F,
        call = 0x10,
        call_indirect = 0x11,
        //Reserved: 0x12 - 0x19
        drop = 0x1A,
        select = 0x1B,
        //Reserved: 0x1C - 0x1F
        get_local = 0x20,
        set_local = 0x21,
        tee_local = 0x22,
        get_global = 0x23,
        set_global = 0x24,
        //Reserved: 0x25 - 0x27
        i32load = 0x28,
        i64load = 0x29,
        f32load = 0x2A,
        f64load = 0x2B,
        i32load8_s = 0x2C,
        i32load8_u = 0x2D,
        i32load16_s = 0x2E,
        i32load16_u = 0x2F,
        i64load8_s = 0x30,
        i64load8_u = 0x31,
        i64load16_s = 0x32,
        i64load16_u = 0x33,
        i64load32_s = 0x34,
        i64load32_u = 0x35,
        i32store = 0x36,
        i64store = 0x37,
        f32store = 0x38,
        f64store = 0x39,
        i32store8 = 0x3A,
        i32store16 = 0x3B,
        i64store8 = 0x3C,
        i64store16 = 0x3D,
        i64store32 = 0x3E,
        current_memory = 0x3F,
        grow_memory = 0x40,
        i32const = 0x41,
        i64const = 0x42,
        f32const = 0x43,
        f64const = 0x44,

        i32eqz = 0x45,
        i32eq = 0x46,
        i32ne = 0x47,
        i32lt_s = 0x48,
        i32lt_u = 0x49,
        i32gt_s = 0x4A,
        i32gt_u = 0x4B,
        i32le_s = 0x4C,
        i32le_u = 0x4D,
        i32ge_s = 0x4E,
        i32ge_u = 0x4F,

        i64eqz = 0x50,
        i64eq = 0x51,
        i64ne = 0x52,
        i64lt_s = 0x53,
        i64lt_u = 0x54,
        i64gt_s = 0x55,
        i64gt_u = 0x56,
        i64le_s = 0x57,
        i64le_u = 0x58,
        i64ge_s = 0x59,
        i64ge_u = 0x5A,

        f32eq = 0x5B,
        f32ne = 0x5C,
        f32lt = 0x5D,
        f32gt = 0x5E,
        f32le = 0x5F,
        f32ge = 0x60,

        f64eq = 0x61,
        f64ne = 0x62,
        f64lt = 0x63,
        f64gt = 0x64,
        f64le = 0x65,
        f64ge = 0x66,

        i32clz = 0x67,
        i32ctz = 0x68,
        i32popcnt = 0x69,
        i32add = 0x6A,
        i32sub = 0x6B,
        i32mul = 0x6C,
        i32div_s = 0x6D,
        i32div_u = 0x6E,
        i32rem_s = 0x6F,
        i32rem_u = 0x70,
        i32and = 0x71,
        i32or = 0x72,
        i32xor = 0x73,
        i32shl = 0x74,
        i32shr_s = 0x75,
        i32shr_u = 0x76,
        i32rotl = 0x77,
        i32rotr = 0x78,

        i64clz = 0x79,
        i64ctz = 0x7A,
        i64popcnt = 0x7B,
        i64add = 0x7C,
        i64sub = 0x7D,
        i64mul = 0x7E,
        i64div_s = 0x7F,
        i64div_u = 0x80,
        i64rem_s = 0x81,
        i64rem_u = 0x82,
        i64and = 0x83,
        i64or = 0x84,
        i64xor = 0x85,
        i64shl = 0x86,
        i64shr_s = 0x87,
        i64shr_u = 0x88,
        i64rotl = 0x89,
        i64rotr = 0x8A,

        f32abs = 0x8B,
        f32neg = 0x8C,
        f32ceil = 0x8D,
        f32floor = 0x8E,
        f32trunc = 0x8F,
        f32nearest = 0x90,
        f32sqrt = 0x91,
        f32add = 0x92,
        f32sub = 0x93,
        f32mul = 0x94,
        f32div = 0x95,
        f32min = 0x96,
        f32max = 0x97,
        f32copysign = 0x98,

        f64abs = 0x99,
        f64neg = 0x9A,
        f64ceil = 0x9B,
        f64floor = 0x9C,
        f64trunc = 0x9D,
        f64nearest = 0x9E,
        f64sqrt = 0x9F,
        f64add = 0xA0,
        f64sub = 0xA1,
        f64mul = 0xA2,
        f64div = 0xA3,
        f64min = 0xA4,
        f64max = 0xA5,
        f64copysign = 0xA6,

        i32wrap_i64 = 0xA7,
        i32trunc_s_f32 = 0xA8,
        i32trunc_u_f32 = 0xA9,
        i32trunc_s_f64 = 0xAA,
        i32trunc_u_f64 = 0xAB,
        i64extend_s_i32 = 0xAC,
        i64extend_u_i32 = 0xAD,
        i64trunc_s_f32 = 0xAE,
        i64trunc_u_f32 = 0xAF,
        i64trunc_s_f64 = 0xB0,
        i64trunc_u_f64 = 0xB1,
        f32convert_s_i32 = 0xB2,
        f32convert_u_i32 = 0xB3,
        f32convert_s_i64 = 0xB4,
        f32convert_u_i64 = 0xB5,
        f32demote_f64 = 0xB6,
        f64convert_s_i32 = 0xB7,
        f64convert_u_i32 = 0xB8,
        f64convert_s_i64 = 0xB9,
        f64convert_u_i64 = 0xBA,
        f64promote_f32 = 0xBB,
        i32reinterpret_f32 = 0xBC,
        i64reinterpret_f64 = 0xBD,
        f32reinterpret_i32 = 0xBE,
        f64reinterpret_i64 = 0xBF
    }
}
