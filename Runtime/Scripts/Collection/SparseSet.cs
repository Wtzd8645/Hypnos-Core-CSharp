using System;

namespace Blanketmen.Hypnos.Collection
{
    public class SparseSet<T>
    {
        private struct Node
        {
            public int index;
            public int key;
            public T value;
        }

        private readonly Node[] nodes;
        private int count;

        public ref T this[int key] => ref nodes[nodes[key].index].value;
        public int Capacity => nodes.Length;
        public int Count => count;

        public SparseSet(int cap = 8)
        {
            nodes = new Node[cap];
        }

        public void Clear()
        {
            for (int i = 0; i < count; ++i)
            {
                nodes[i].key = default;
                nodes[i].value = default;
            }

            count = 0;
        }

        public void Insert(int key, T value)
        {
            if (key < 0 || key >= nodes.Length)
            {
                return;
            }

            int index = nodes[key].index;
            if (index < count && nodes[index].key == key)
            {
                return;
            }

            nodes[key].index = count;
            nodes[count].key = key;
            nodes[count++].value = value;
        }

        public ref T Insert(int key)
        {
            if (key < 0 || key >= nodes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            int index = nodes[key].index;
            if (index < count && nodes[index].key == key)
            {
                return ref nodes[index].value;
            }

            nodes[key].index = count;
            nodes[count].key = key;
            return ref nodes[count++].value;
        }

        public void Remove(int key)
        {
            if (key < 0 || key >= nodes.Length)
            {
                return;
            }

            int index = nodes[key].index;
            if (index >= count || nodes[index].key != key)
            {
                return;
            }

            if (index < --count)
            {
                int lastKey = nodes[count].key;
                nodes[lastKey].index = index;
                nodes[index].key = lastKey;
                nodes[index].value = nodes[count].value;
            }
            nodes[count].value = default;
        }
    }
}