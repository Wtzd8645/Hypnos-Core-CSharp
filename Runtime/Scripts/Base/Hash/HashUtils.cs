namespace Blanketmen.Hypnos.Hash
{
    public static class HashUtils
    {
        public static uint BkdrHash(byte[] bytes, uint seed = 31)
        {
            uint hash = 0;
            for (int i = 0, len = bytes.Length; i < len; i++)
            {
                hash = hash * seed + bytes[i];
            }
            return hash;
        }

        public static uint Fnv1aHash(byte[] bytes)
        {
            uint hash = 2166136261u;
            for (int i = 0, len = bytes.Length; i < len; i++)
            {
                hash ^= bytes[i];
                hash *= 16777619u; // hash = (hash << 24) + (hash << 8) + (hash << 7) + (hash << 4) + (hash << 1) + hash;
            }
            return hash;
        }
    }
}