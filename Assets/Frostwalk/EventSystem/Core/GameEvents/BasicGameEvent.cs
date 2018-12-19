using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Basic Game Event", order = 0)]
public class BasicGameEvent : GameEvent {

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(this);
        }
    }
}
