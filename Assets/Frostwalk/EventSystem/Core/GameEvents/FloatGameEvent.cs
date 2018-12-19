using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Float Game Event", order = 2)]
public class FloatGameEvent : GameEvent
{
    public float SentFloat { get; private set; }

    public void Raise(float f)
    {
        SentFloat = f;
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(this);
        }
    }
}
