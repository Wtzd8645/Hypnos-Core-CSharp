using System.Collections.Generic;

namespace Blanketmen.Hypnos.Cache
{
    public class LruCache<TKey, TValue> where TValue : class
    {
        private struct Node
        {
            public TKey key;
            public TValue value;
            public int prev;
            public int next;
        }

        private readonly Dictionary<TKey, int> nodeMap;
        private readonly Node[] nodes;
        private int head = -1;
        private int tail = -1;

        public int Capacity => nodes.Length;
        public int Count => nodeMap.Count;

        public LruCache(int cap = 8)
        {
            nodes = new Node[cap];
            for (int i = 0; i < cap; ++i)
            {
                nodes[i].next = i + 1;
            }
            nodes[cap - 1].next = -1;
            nodeMap = new Dictionary<TKey, int>(cap);
        }

        public void Clear()
        {
            nodeMap.Clear();
            for (int i = 0; i < nodes.Length; ++i)
            {
                nodes[i].key = default;
                nodes[i].value = default;
                nodes[i].next = i + 1;
            }
            nodes[nodes.Length - 1].next = -1;
            head = -1;
            tail = -1;
        }

        private void MoveToFront(int index)
        {
            if (index == head)
            {
                return;
            }

            Node node = nodes[index];
            nodes[node.prev].next = node.next;
            if (index == tail)
            {
                tail = node.prev;
            }
            else
            {
                nodes[node.next].prev = node.prev;
            }

            nodes[index].prev = -1;
            nodes[index].next = head;

            nodes[head].prev = index;
            head = index;
        }

        public TValue Get(TKey key)
        {
            if (nodeMap.TryGetValue(key, out int index))
            {
                MoveToFront(index);
                return nodes[index].value;
            }

            return null;
        }

        public void Put(TKey key, TValue value)
        {
            if (nodeMap.TryGetValue(key, out int index))
            {
                nodes[index].value = value;
                MoveToFront(index);
                return;
            }

            if (Count >= Capacity)
            {
                nodeMap.Remove(nodes[tail].key);
                nodeMap[key] = tail;
                nodes[tail].key = key;
                nodes[tail].value = value;
                MoveToFront(tail);
                return;
            }

            index = Count;
            nodeMap[key] = index;
            nodes[index].key = key;
            nodes[index].value = value;
            nodes[index].prev = -1;
            nodes[index].next = head;

            if (head < 0)
            {
                tail = index;
            }
            else
            {
                nodes[head].prev = index;
            }
            head = index;
        }
    }
}