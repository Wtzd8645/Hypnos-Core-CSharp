using System;
using System.Runtime.CompilerServices;

namespace Blanketmen.Hypnos.Collection
{
    public class ArrayBackedLinkedList<T>
    {
        private struct Node
        {
            public T item;
            public int prev;
            public int next;
        }

        private Node[] nodes;
        private int count;
        private int free;
        private int head = -1;
        private int tail = -1;

        public ref T this[int index] => ref nodes[index].item;
        public int Capacity => nodes.Length;
        public int Count => count;
        public int Head => head;

        public ArrayBackedLinkedList(int cap = 8)
        {
            nodes = new Node[cap];
            for (int i = 0; i < cap; ++i)
            {
                nodes[i].next = i + 1;
            }
            nodes[cap - 1].next = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetNext(int index) => index >= 0 && index < nodes.Length ? nodes[index].next : -1;

        public void Clear()
        {
            for (int i = head; i >= 0;)
            {
                int next = nodes[i].next;
                nodes[i].item = default;
                nodes[i].next = free;
                free = i;
                i = next;
            }

            count = 0;
            head = -1;
            tail = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureCapacity()
        {
            if (free >= 0)
            {
                return;
            }

            int oldCap = nodes.Length;
            int newCap = oldCap * 2;
            if ((uint)newCap > int.MaxValue)
            {
                newCap = int.MaxValue;
            }

            Node[] newNodes = new Node[newCap];
            Array.Copy(nodes, newNodes, oldCap);
            for (int i = oldCap; i < newCap; ++i)
            {
                newNodes[i].next = i + 1;
            }
            newNodes[newCap - 1].next = -1;
            nodes = newNodes;
            free = oldCap;
        }

        public int Add(T item)
        {
            EnsureCapacity();

            ++count;
            int index = free;
            free = nodes[index].next;

            nodes[index].item = item;
            nodes[index].prev = tail;
            nodes[index].next = -1;

            if (head < 0)
            {
                head = index;
            }
            else
            {
                nodes[tail].next = index;
            }
            tail = index;
            return index;
        }

        public ref T Add(out int index)
        {
            EnsureCapacity();

            ++count;
            index = free;
            free = nodes[index].next;

            nodes[index].prev = tail;
            nodes[index].next = -1;

            if (head < 0)
            {
                head = index;
            }
            else
            {
                nodes[tail].next = index;
            }
            tail = index;
            return ref nodes[index].item;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= nodes.Length)
            {
                return;
            }

            --count;
            Node node = nodes[index];
            if (index == head)
            {
                head = node.next;
            }
            else
            {
                nodes[node.prev].next = node.next;
            }

            if (index == tail)
            {
                tail = node.prev;
            }
            else
            {
                nodes[node.next].prev = node.prev;
            }

            nodes[index].item = default;
            nodes[index].next = free;
            free = index;
        }
    }
}