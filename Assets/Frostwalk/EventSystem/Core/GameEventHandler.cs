using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[Serializable]
public class GameEventHandler
{
    public GameEvent gameEvent;
    public ListenerFilters listenerFilter;
   
    bool HasBasicFlag => this.listenerFilter.HasFlag(ListenerFilters.Basic);
    bool HasFloatFlag => this.listenerFilter.HasFlag(ListenerFilters.Float);
    bool HasStatsFlag => this.listenerFilter.HasFlag(ListenerFilters.Stats);

    [SerializeField] [ShowIf("HasBasicFlag")] UnityEvent basicResponse;
    [SerializeField] [ShowIf("HasFloatFlag")] FloatEvent floatResponse;
    [SerializeField] [ShowIf("HasStatsFlag")] StatsEvent statsResponse;

    public void OnEventRaised(GameEvent ge)
    {
        if (ge is BasicGameEvent && basicResponse.GetPersistentEventCount() >= 1)
            basicResponse.Invoke();

        if (ge is FloatGameEvent && floatResponse.GetPersistentEventCount() >= 1)
            floatResponse.Invoke(((FloatGameEvent)ge).SentFloat);

        if (ge is StatsGameEvent && statsResponse.GetPersistentEventCount() >= 1)
            statsResponse.Invoke(((StatsGameEvent)ge).SentStats, ((StatsGameEvent)ge).SentFloat);       
    }

}

[Flags]
public enum ListenerFilters
{
    None = 0,
    Basic = 1 << 1,
    Float = 1 << 2,
    Stats = 1 << 3
}
