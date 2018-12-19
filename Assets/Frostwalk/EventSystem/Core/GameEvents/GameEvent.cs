using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{
    protected List<GameEventListeners> listeners = new List<GameEventListeners>();

    public void RegisterListener(GameEventListeners handler)
    {
        if (!listeners.Contains(handler))
            listeners.Add(handler);
    }

    public void UnregisterListener(GameEventListeners handler)
    {
        if (listeners.Contains(handler))
            listeners.Remove(handler);
    }
}
