using Blanketmen.Hypnos.Collection;
using NUnit.Framework;

namespace Blanketmen.Hypnos.Tests.Core
{
    internal class CollectionTests
    {
        private struct TestStruct
        {
            public static bool operator ==(TestStruct lhs, TestStruct rhs) => lhs.a == rhs.a;
            public static bool operator !=(TestStruct lhs, TestStruct rhs) => lhs.a != rhs.a;

            public int a;

            public override bool Equals(object obj) => obj is TestStruct s && this == s;
            public override int GetHashCode() => a;
        }

        [Test]
        public void ArrayBackedLinkedListPasses()
        {
            TestStruct[] result = new TestStruct[]
            {
                new TestStruct { a = 1 },
                new TestStruct { a = 3 },
                new TestStruct { a = 5 },
                new TestStruct { a = 7 },
                new TestStruct { a = 0 },
                new TestStruct { a = 2 },
                new TestStruct { a = 4 },
                new TestStruct { a = 6 },
            };

            ArrayBackedLinkedList<TestStruct> list = new ArrayBackedLinkedList<TestStruct>(3);
            for (int i = 0; i < result.Length; ++i)
            {
                list.Add(new TestStruct { a = i });
            }

            for (int i = list.Head; i >= 0;)
            {
                int next = list.GetNext(list.GetNext(i));
                list.Remove(i);
                i = next;
            }

            for (int i = 0; i < result.Length; i += 2)
            {
                list.Add(new TestStruct { a = i });
            }

            for (int i = list.Head, j = 0; i > 0; i = list.GetNext(i), ++j)
            {
                if (list[i] != result[j])
                {
                    Assert.Fail();
                    return;
                }
            }

            list.Clear();
            for (int i = 0; i < result.Length; ++i)
            {
                list.Add(result[i]);
            }

            for (int i = list.Head, j = 0; i > 0; i = list.GetNext(i), ++j)
            {
                if (list[i] != result[j])
                {
                    Assert.Fail();
                    return;
                }
            }

            Assert.Pass();
        }

        [Test]
        public void CircularArrayPasses()
        {
            int cap = 5;
            int num = 13;
            CircularArray<int> arr = new CircularArray<int>(cap);
            for (int i = 0; i < num; ++i)
            {
                arr.EnqueueBack(i);
            }

            if (!arr.TryPeekFront(out int item) || item != num - cap)
            {
                Assert.Fail();
                return;
            }

            if (!arr.TryPeekBack(out item) || item != num - 1)
            {
                Assert.Fail();
                return;
            }

            for (int i = 0; i < 7; ++i)
            {
                arr.TryDequeueBack(out _);
                arr.EnqueueFront(i);
                arr.TryDequeueFront(out _);
            }

            arr.EnqueueFront(999);
            if (!arr.TryPeekFront(out int a) || !arr.TryPeekBack(out int b) || a != b)
            {
                Assert.Fail();
                return;
            }

            Assert.Pass();
        }

        [Test]
        public void SparseSetPasses()
        {
            int[] result = new int[] { 10, 20, 30, 40, 50, 60, 70 };

            int cap = 7;
            SparseSet<int> set = new SparseSet<int>(cap);
            for (int i = 0; i < cap; ++i)
            {
                set.Insert(i, result[i]);
            }

            for (int i = 0; i < cap; i += 2)
            {
                set.Remove(i);
            }

            for (int i = 0; i < cap; i += 2)
            {
                set.Insert(i, result[i]);
            }

            for (int i = 0; i < result.Length; ++i)
            {
                if (set[i] != result[i])
                {
                    Assert.Fail();
                    return;
                }
            }

            Assert.Pass();
        }
    }
}