using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameEvent : ScriptableObject
{
    public List<EventListener> _eventListeners = new List<EventListener>();


    public void Subscribe(EventListener eventListener)
    {
        if (!_eventListeners.Contains(eventListener))
        {
            _eventListeners.Add(eventListener);
        }
    }

    public void Unsubscribe(EventListener eventListener)
    {
        if (_eventListeners.Contains(eventListener))
        {
            _eventListeners.Remove(eventListener);
        }
    }

    public void Raise()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnEventRaise();
        }
    }
}
