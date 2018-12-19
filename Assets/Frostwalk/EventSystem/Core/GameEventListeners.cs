using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListeners : MonoBehaviour
{
    [SerializeField] List<GameEventHandler> listeners;

    private void OnEnable()
    {
        if (listeners.Count >= 1)
        {
            foreach(GameEventHandler h in listeners)
            {
                h.gameEvent.RegisterListener(this);
            }
            
        }
    }

    private void OnDisable()
    {
        if (listeners.Count >= 1)
        {
            foreach (GameEventHandler h in listeners)
            {
                h.gameEvent.UnregisterListener(this);
            }

        }
    }

    public void OnEventRaised(GameEvent passedEvent)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (passedEvent == listeners[i].gameEvent)
            {
                listeners[i].OnEventRaised(passedEvent);
            }
        }
    }

}
