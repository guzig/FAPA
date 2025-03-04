using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;

namespace FaPA.Infrastructure.Utils
{
    public class EventPublisher
    {
        private static readonly IDictionary<Type, List<Action<object>>> Subscribers
            = new Dictionary<Type, List<Action<object>>>();

        public static void Publish<T>(T eventToPublish, IDispose publishedBy)
        {
            List<Action<object>> actions;
            lock (Subscribers)
            {
                if (Subscribers.TryGetValue(typeof(T), out actions) == false)
                {
                    return;
                }
            }
            foreach (var action in actions)
            {
                if (action.Target == publishedBy)
                    continue;
                action(eventToPublish);
            }
        }

        public static void Register<T>(Action<T> action)
        {
            Debug.Assert(action.Target is IDispose);
            lock (Subscribers)
            {
                List<Action<object>> value;
                if (Subscribers.TryGetValue(typeof(T), out value) == false)
                {
                    Subscribers[typeof(T)] = value = new List<Action<object>>();
                }
                var dispatcher = Dispatcher.CurrentDispatcher;
                Debug.Assert(dispatcher != null);
                Action<object> item = o =>
                {
                    if (dispatcher != Dispatcher.CurrentDispatcher)
                    {
                        dispatcher.Invoke(delegate
                        {
                            action((T)o);
                        });
                    }
                    else
                    {
                        action((T)o);
                    }
                };
                ((IDispose)action.Target).Disposed += () => Unregister<T>(item);
                value.Add(item);
            }
        }


        private static void Unregister<T>(Action<object> action)
        {
            lock (Subscribers)
            {
                List<Action<object>> value;
                if (Subscribers.TryGetValue(typeof(T), out value) == false)
                {
                    return;
                }
                value.Remove(action);
            }
        }
    }
}