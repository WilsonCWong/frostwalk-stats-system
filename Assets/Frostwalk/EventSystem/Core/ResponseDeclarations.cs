using UnityEngine.Events;
using Frostwalk.StatSystem;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[System.Serializable]
public class StatsEvent : UnityEvent<Stat, float> { }
