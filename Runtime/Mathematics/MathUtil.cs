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
        public static int Clamp(int value, int min, int max) => value < min ? min : value > max ? max : value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine(short hi, short lo) => hi << 16 | (ushort)lo;

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

        public static int GetDecimalPlaces(float minDiff)
        {
            if (minDiff <= 0)
            {
                return 0;
            }

            int decimalPlaces = 0;
            while (minDiff < 1 && decimalPlaces < MaxDecimals)
            {
                minDiff *= 10;
                decimalPlaces++;
            }
            return decimalPlaces;
        }
    }
}