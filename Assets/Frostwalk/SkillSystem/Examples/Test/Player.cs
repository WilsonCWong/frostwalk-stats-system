using UnityEngine;
using Frostwalk.StatSystem;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public class Player : MonoBehaviour
{
    [SerializeField] StatSystem statAsset;

    [SerializeField] StatSystem stats;

    private void Awake()
    {
        stats = Object.Instantiate(statAsset);
    }
    private void OnEnable()
    {
        stats.Skills["Sneak"].OnAddExp += new AddExpHandler(LogExpGain);
        stats.Skills["Sneak"].OnAddSkillPoints += new AddSkillPointsHandler(LogSkillPointsGain);
        stats.Skills["Sneak"].OnLevelUp += new LevelUpHandler(LogLevelUp);

    }

    void OnDisable()
    {
        stats.Skills["Sneak"].OnAddExp -= new AddExpHandler(LogExpGain);
        stats.Skills["Sneak"].OnAddSkillPoints -= new AddSkillPointsHandler(LogSkillPointsGain);
        stats.Skills["Sneak"].OnLevelUp -= new LevelUpHandler(LogLevelUp);
    }

    public void LogExpGain(Stat s, float exp)
    {
        Debug.Log("Gained " + exp + " experience towards " + s.Name + ".");
        Debug.Log("Exp to next level: " + s.ExpToNext);
    }

    public void LogSkillPointsGain(Stat s, float pointsGained)
    {
        Debug.Log("Gained " + pointsGained + " points towards " + s.Name + ".");
    }

    public void LogLevelUp(Stat s, float levelsGained)
    {
        Debug.Log("Gained " + levelsGained + " levels for " + s.Name + ".");
        Debug.Log(s.Name + " is now Level " + s.CurrentPoints + ".");
    }

}


