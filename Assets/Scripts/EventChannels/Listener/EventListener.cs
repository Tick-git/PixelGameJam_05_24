using System;
using UnityEngine;
using UnityEngine.Events;

public class EventListener<T> : MonoBehaviour
{
    [SerializeField] EventChannel<T> _channel;
    [SerializeField] UnityEvent<T> _unityEvent;

    private void Awake()
    {
        _channel.RegisterListener(this);
    }

    private void OnDestroy()
    {
        _channel.UnregisterListener(this);
    }

    internal void Raise(T value)
    {
        _unityEvent.Invoke(value);
    }
}
