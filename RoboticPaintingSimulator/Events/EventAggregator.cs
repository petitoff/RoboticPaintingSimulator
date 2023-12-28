using System;
using System.Collections.Generic;
using RoboticPaintingSimulator.ViewModels;

namespace RoboticPaintingSimulator.Events;

public class EventAggregator
{
    private static EventAggregator _instance;
    public static EventAggregator Instance => _instance ?? (_instance = new EventAggregator());

    private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new Dictionary<Type, List<Delegate>>();

    public void Subscribe<T>(Action<T> handler)
    {
        if (_eventHandlers.ContainsKey(typeof(T)))
        {
            _eventHandlers[typeof(T)].Add(handler);
        }
        else
        {
            _eventHandlers[typeof(T)] = new List<Delegate> { handler };
        }
    }

    public void Publish<T>(T message)
    {
        if (_eventHandlers.TryGetValue(typeof(T), out var handlers))
        {
            foreach (var handler in handlers)
            {
                if (handler is Action<T> action)
                {
                    action.Invoke(message);
                }
            }
        }
    }
}

