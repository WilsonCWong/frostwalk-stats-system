using UnityEngine;
using Frostwalk.StatSystem;

[CreateAssetMenu(menuName = "Game Events/Stats Game Event", order = 1)]
public class StatsGameEvent : GameEvent
{
    public Stat SentStats { get; private set; }
    public float SentFloat { get; private set; }

    public void Raise(Stat s, float f)
    {
        SentStats = s;
        SentFloat = f;

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(this);
        }
    }
}
