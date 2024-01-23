using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public string Id { get; set; }
        
        public MonsterTypeId MonsterTypeId;

        public Transform StartPoint;
        public Transform EndPoint;

        private bool _slain;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;
        
        [Inject]
        public void Construct(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if(_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(MonsterTypeId, transform, StartPoint, EndPoint);

            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null) 
                _enemyDeath.Happened -= Slay;
            
            _slain = true;
        }
    }
}