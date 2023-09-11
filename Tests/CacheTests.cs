using Blanketmen.Hypnos.Cache;
using NUnit.Framework;

namespace Blanketmen.Hypnos.Tests.Core
{
    internal class CacheTests
    {
        private class TestClass
        {
            public int value;
        }

        [Test]
        public void LruCachePasses()
        {
            LruCache<int, TestClass> cache = new LruCache<int, TestClass>(3);
            TestClass[] classes = new TestClass[]
            {
                new TestClass() { value = 0 },
                new TestClass() { value = 1 },
                new TestClass() { value = 2 },
                new TestClass() { value = 3 },
                new TestClass() { value = 4 },
                new TestClass() { value = 5 },
                new TestClass() { value = 6 },
                new TestClass() { value = 7 },
                new TestClass() { value = 8 },
                new TestClass() { value = 9 },
            };

            cache.Put(0, classes[0]);
            cache.Put(1, classes[1]);
            if (cache.Get(0) == null)
            {
                Assert.Fail();
                return;
            }

            cache.Put(2, classes[2]);
            cache.Put(3, classes[3]);
            cache.Put(4, classes[4]);
            if (cache.Get(0) != null)
            {
                Assert.Fail();
                return;
            }

            cache.Clear();
            cache.Put(5, classes[5]);
            cache.Put(6, classes[6]);
            cache.Put(7, classes[7]);
            cache.Put(8, classes[8]);
            cache.Put(9, classes[9]);
            if (cache.Get(7) != classes[7])
            {
                Assert.Fail();
                return;
            }

            Assert.Pass();
        }
    }
}