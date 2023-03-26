namespace Blanketmen.Hypnos.Memory
{
    public static class MemoryUtil
    {
        public static int Align(int pos, int align)
        {
            // TODO: return (n + 7) & static_cast<size_t>(-8); // Align n to next multiple of 8 (from Hacker's Delight, Chapter 3.)
            // NOTE: -offset & (Align - 1) equal to (Align - (offset % Align)) % Align;
            return pos + (-pos & (align - 1));
        }
    }
}