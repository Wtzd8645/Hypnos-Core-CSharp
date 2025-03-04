using System;
using System.Collections.Generic;

namespace Blanketmen.Hypnos.Mediation
{
    /// <summary>
    /// A generic event dispatcher that allows registering, unregistering, and invoking events.
    /// </summary>
    /// <typeparam name="TKey">The key type used to identify events.</typeparam>
    /// <remarks>
    /// This dispatcher ensures that each event key is associated with a consistent delegate type.
    /// NOTE: There is GC overhead when delegates are merged.
    /// </remarks>
    public class EventDispatcher<TKey>
    {
        private readonly Dictionary<TKey, Delegate> handlerMap;

        public EventDispatcher()
        {
            handlerMap = new Dictionary<TKey, Delegate>();
        }

        public EventDispatcher(IEqualityComparer<TKey> comparer)
        {
            handlerMap = new Dictionary<TKey, Delegate>(comparer);
        }

        public void Clear()
        {
            handlerMap.Clear();
        }

        public void Register(TKey id, Action handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                handlerMap[id] = handler;
                return;
            }

            if (del is Action handlers)
            {
                handlerMap[id] = handlers + handler;
                return;
            }

            throw new InvalidOperationException($"[EventDispatcher] Cannot register different types of handlers with the same key. Id: {id}");
        }

        public void Unregister(TKey id, Action handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del is Action handlers)
            {
                handlerMap[id] = handlers - handler;
            }
        }

        protected void Notify(TKey id)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            (del as Action)?.Invoke();
        }

        public void Register<T1>(TKey id, Action<T1> handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                handlerMap[id] = handler;
                return;
            }

            if (del is Action<T1> handlers)
            {
                handlerMap[id] = handlers + handler;
                return;
            }

            throw new InvalidOperationException($"[EventDispatcher] Cannot register different types of handlers with the same key. Id: {id}");
        }

        public void Unregister<T1>(TKey id, Action<T1> handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del is Action<T1> handlers)
            {
                handlerMap[id] = handlers - handler;
            }
        }

        protected void Notify<T1>(TKey id, T1 arg)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            (del as Action<T1>)?.Invoke(arg);
        }

        public void Register<T1, T2>(TKey id, Action<T1, T2> handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                handlerMap[id] = handler;
                return;
            }

            if (del is Action<T1, T2> handlers)
            {
                handlerMap[id] = handlers + handler;
            }

            throw new InvalidOperationException($"[EventDispatcher] Cannot register different types of handlers with the same key. Id: {id}");
        }

        public void Unregister<T1, T2>(TKey id, Action<T1, T2> handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2> handlers)
            {
                handlerMap[id] = handlers - handler;
            }
        }

        protected void Notify<T1, T2>(TKey id, T1 arg1, T2 arg2)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            (del as Action<T1, T2>)?.Invoke(arg1, arg2);
        }

        public void Register<T1, T2, T3>(TKey id, Action<T1, T2, T3> handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                handlerMap[id] = handler;
                return;
            }

            if (del is Action<T1, T2, T3> handlers)
            {
                handlerMap[id] = handlers + handler;
                return;
            }

            throw new InvalidOperationException($"[EventDispatcher] Cannot register different types of handlers with the same key. Id: {id}");
        }

        public void Unregister<T1, T2, T3>(TKey id, Action<T1, T2, T3> handler)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2, T3> handlers)
            {
                handlerMap[id] = handlers - handler;
            }
        }

        protected void Notify<T1, T2, T3>(TKey id, T1 arg1, T2 arg2, T3 arg3)
        {
            handlerMap.TryGetValue(id, out Delegate del);
            (del as Action<T1, T2, T3>)?.Invoke(arg1, arg2, arg3);
        }
    }
}