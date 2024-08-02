using System;
using System.Runtime.CompilerServices;

namespace Blanketmen.Hypnos.Mathematics
{
    public static partial class MathUtil
    {
        private const int MaxDecimals = 15;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CeilToInt(double value) => (int)Math.Ceiling(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FloorToInt(double value) => (int)Math.Floor(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(short hi, short lo)
        {
            return hi << 16 | (ushort)lo;
        }

        public static int RoundUpToPowerOfTwo(int i)
        {
            --i;
            i |= i >> 1;
            i |= i >> 2;
            i |= i >> 4;
            i |= i >> 8;
            i |= i >> 16;
            return i + 1;
        }

        public static int GetNumberOfDecimalsForMinimumDifference(float minDiff)
        {
            return Math.Clamp(-FloorToInt(Math.Log10(Math.Abs(minDiff))), 0, MaxDecimals);
        }
    }
}