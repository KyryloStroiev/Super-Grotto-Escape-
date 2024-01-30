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
        private const string StaticDataLevels = "StaticData/Levels";
        private const string StaticDataMonsters = "StaticData/Monsters";
        private const string StaticDataPlayer = "StaticData/Player";
        private Dictionary<MonsterTypeId,MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<string, PlayerData> _player;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(StaticDataMonsters)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevels)
                .ToDictionary(x => x.LevelKey, x => x);

            _player = Resources
                .LoadAll<PlayerData>(StaticDataPlayer)
                .ToDictionary(x => x.Player, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            GetData(typeId, _monsters);

        public LevelStaticData ForLevel(string sceneKey) =>
            GetData(sceneKey, _levels);

        public PlayerData ForPlayer(string player) =>
            GetData(player, _player);
        private T GetData<T, TKey>(TKey key, Dictionary<TKey, T> dictionary) where T : class => 
            dictionary.TryGetValue(key, out T staticData) ? staticData : null;
    }
}