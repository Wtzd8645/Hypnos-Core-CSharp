namespace Blanketmen.Hypnos.Hash
{
    public static class HashUtils
    {
        public static uint BkdrHash(byte[] bytes)
        {
            uint hash = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                hash = hash * 31 + bytes[i];
            }
            return hash;
        }
    }
}