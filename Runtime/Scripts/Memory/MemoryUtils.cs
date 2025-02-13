using System.Runtime.CompilerServices;

namespace Blanketmen.Hypnos.Memory
{
    public static class MemoryUtils
    {
        // TODO: return (n + 7) & static_cast<size_t>(-8); // Align n to next multiple of 8 (from Hacker's Delight, Chapter 3.)

        /// <summary>
        /// Aligns the specified value up to the nearest multiple of the alignment.
        /// </summary>
        /// <param name="val">The value to align.</param>
        /// <param name="align">The alignment value, which must be a power of two.</param>
        /// <returns>The smallest integer greater than or equal to <paramref name="val"/> that is a multiple of <paramref name="align"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AlignUp(int val, int align) => val + (-val & (align - 1));

        /// <summary>
        /// Aligns the specified value down to the nearest multiple of the alignment.
        /// </summary>
        /// <param name="val">The value to align.</param>
        /// <param name="align">The alignment value, which must be a power of two.</param>
        /// <returns>The largest integer less than or equal to <paramref name="val"/> that is a multiple of <paramref name="align"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AlignDown(int val, int align) => val & ~(align - 1);
    }
}