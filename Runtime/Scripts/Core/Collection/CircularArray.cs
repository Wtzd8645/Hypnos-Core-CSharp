using System;

namespace Blanketmen.Hypnos.Collection
{
    public class CircularArray<T>
    {
        private readonly T[] items;
        private int count;
        private int head = -1;
        private int tail = -1;

        public int Capacity => items.Length;
        public int Count => count;

        public CircularArray(int cap = 8)
        {
            items = new T[cap];
            count = 0;
        }

        public void Clear()
        {
            Array.Clear(items, 0, items.Length);
            count = 0;
            head = -1;
            tail = -1;
        }

        public bool TryPeekFront(out T item)
        {
            if (count == 0)
            {
                item = default;
                return false;
            }

            item = items[head];
            return true;
        }

        public bool TryPeekBack(out T item)
        {
            if (count == 0)
            {
                item = default;
                return false;
            }

            item = items[tail];
            return true;
        }

        public void EnqueueFront(T item)
        {
            if (count == 0)
            {
                ++count;
                items[0] = item;
                head = 0;
                tail = 0;
                return;
            }

            if (--head < 0)
            {
                head = items.Length - 1;
            }
            items[head] = item;

            if (head != tail)
            {
                ++count;
                return;
            }

            if (--tail < 0)
            {
                tail = items.Length - 1;
            }
        }

        public void EnqueueBack(T item)
        {
            if (count == 0)
            {
                ++count;
                items[0] = item;
                head = 0;
                tail = 0;
                return;
            }

            if (++tail == items.Length)
            {
                tail = 0;
            }
            items[tail] = item;

            if (head != tail)
            {
                ++count;
                return;
            }

            if (++head == items.Length)
            {
                head = 0;
            }
        }

        public bool TryDequeueFront(out T item)
        {
            if (count == 0)
            {
                item = default;
                return false;
            }

            --count;
            item = items[head];
            items[head] = default;
            if (++head == items.Length)
            {
                head = 0;
            }
            return true;
        }

        public bool TryDequeueBack(out T item)
        {
            if (count == 0)
            {
                item = default;
                return false;
            }

            --count;
            item = items[tail];
            items[tail] = default;
            if (--tail < 0)
            {
                tail = items.Length - 1;
            }
            return true;
        }
    }
}