using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
    readonly HashSet<EventListener<T>> _listeners = new();

    public void Invoke(T value)
    {
        foreach (var listener in _listeners)
        {
            listener.Raise(value);
        }
    }

    public void RegisterListener(EventListener<T> listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(EventListener<T> listener)
    {
        _listeners.Remove(listener);
    }
}

public readonly struct Empty { }

[CreateAssetMenu(menuName = "Events/EventChannel")]
public class EventChannel : EventChannel<Empty> { }