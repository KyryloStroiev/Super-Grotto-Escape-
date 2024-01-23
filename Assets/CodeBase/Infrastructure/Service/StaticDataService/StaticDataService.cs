using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Service.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticdataLevels = "StaticData/Levels";
        private const string StaticdataMonsters = "StaticData/Monsters";
        private Dictionary<MonsterTypeId,MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(StaticdataMonsters)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticdataLevels)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            GetData(typeId, _monsters);

        public LevelStaticData ForLevel(string sceneKey) =>
            GetData(sceneKey, _levels);
        
        private T GetData<T, TKey>(TKey key, Dictionary<TKey, T> dictionary) where T : class => 
            dictionary.TryGetValue(key, out T staticData) ? staticData : null;
    }
}