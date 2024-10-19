using System.Runtime.CompilerServices;

namespace Blanketmen.Hypnos.Encoding
{
    public static class ZigZag
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Encode(int n)
        {
            return (uint)((n << 1) ^ (n >> 31));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Encode(long n)
        {
            return (ulong)((n << 1) ^ (n >> 63));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Decode(uint n)
        {
            return (int)(n >> 1) ^ -(int)(n & 1u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Decode(ulong n)
        {
            return (long)(n >> 1) ^ -(long)(n & 1u);
        }
    }
}