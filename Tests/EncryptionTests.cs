using Blanketmen.Hypnos.Encryption;
using NUnit.Framework;

namespace Blanketmen.Hypnos.Tests.Core
{
    internal class EncryptionTests
    {
        [Test]
        public void Aes256Passes()
        {
            Aes256 encryption = new Aes256();
            byte[] data = new byte[96];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = (byte)i;
            }
            byte[] result = encryption.Encrypt(data, 0, data.Length);
            result = encryption.Decrypt(result, 0, result.Length);

            if (result.Length != data.Length)
            {
                Assert.Fail();
                return;
            }

            bool isPass = true;
            for (int i = 0; i < result.Length; ++i)
            {
                if (result[i] != i)
                {
                    isPass = false;
                    break;
                }
            }
            Assert.IsTrue(isPass);
        }
    }
}