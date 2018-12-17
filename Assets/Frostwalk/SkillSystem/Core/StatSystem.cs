using UnityEngine;
using Frostwalk.StatSystem;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Frostwalk.StatSystem
{
    [CreateAssetMenu(fileName = "New Stats", menuName = "Skill System/Stats Object", order = 1)]
    public class StatSystem : SerializedScriptableObject
    {
        [DictionaryDrawerSettings(KeyLabel = "Name", ValueLabel = "Skill")]
        [SerializeField]
        Dictionary<string, Stat> skills;

        public Dictionary<string, Stat> Skills
        {
            get { return skills; }
        }
    }
}
