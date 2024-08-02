using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Blanketmen.Hypnos.Cache
{
    public class ObjectPool<T> where T : class
    {
        private const BindingFlags ConstructorFlags = BindingFlags.Public | BindingFlags.Instance;

        private readonly Func<T> constructor;
        private T[] buffer;
        private int count;

        public int Capacity => buffer.Length;
        public int Count => count;

        public ObjectPool(Func<T> ctor = null, int cap = 8, bool isPopulate = false)
        {
            if (ctor == null)
            {
                ConstructorInfo ctorInfo = typeof(T).GetConstructor(ConstructorFlags, null, Type.EmptyTypes, null);
                NewExpression newExpr = Expression.New(ctorInfo, (IEnumerable<Expression>)null);
                constructor = Expression.Lambda<Func<T>>(newExpr).Compile();
            }
            else
            {
                constructor = ctor;
            }

            buffer = new T[cap];
            if (isPopulate)
            {
                Populate();
            }
        }

        public void Clear()
        {
            Array.Clear(buffer, 0, count);
            count = 0;
        }

        public void Populate()
        {
            while (count < buffer.Length)
            {
                buffer[count++] = constructor();
            }
        }

        public void Push(T obj)
        {
            if (count >= buffer.Length)
            {
                T[] newBuf = new T[count * 2];
                Array.Copy(buffer, newBuf, count);
                buffer = newBuf;
            }

            buffer[count++] = obj;
        }

        public T Pop()
        {
            return count > 0 ? buffer[--count] : constructor();
        }

        public bool TryPush(T obj)
        {
            if (count >= buffer.Length)
            {
                return false;
            }

            buffer[count++] = obj;
            return true;
        }

        public bool TryPop(out T obj)
        {
            if (count == 0)
            {
                obj = null;
                return false;
            }

            obj = buffer[--count];
            return true;
        }
    }
}