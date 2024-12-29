using CodeBase.Enemy;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using CodeBase.UI;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.EnemyFactory
{
    public class MonsterBuilder
    {
        private readonly GameObject _monster;

        public MonsterBuilder(GameObject monster)
        {
            _monster = monster;
        }

        public MonsterBuilder SetHealth(float maxHP)
        {
            IHealth health = _monster.GetComponent<IHealth>();
            health.CurrentHP = maxHP;
            health.MaxHP = maxHP;
            _monster.GetComponent<ActorUI>().Construct(health);
            return this;
        }

        public MonsterBuilder SetMove(float speed, bool isFlying)
        {
            EnemyMove move = _monster.GetComponent<EnemyMove>();
            move.Speed = speed;
            move.IsFlying = isFlying;
            return this;
        }

        public MonsterBuilder SetMelleAttack(float damage, float cooldown, float minDistanceAttack)
        {
            EnemyAttackMelee attackMelee = _monster.GetComponent<EnemyAttackMelee>();
            attackMelee.Damage = damage;
            attackMelee.Cooldown = cooldown;

            _monster.GetComponent<EnemyAggro>().MinDistanceToAttack = minDistanceAttack;

            return this;
        }

        public MonsterBuilder SetObjectPool(IObjectPool objectPool)
        {
            EnemyDeath enemyDeath = _monster.GetComponent<EnemyDeath>();
            enemyDeath.Construct(objectPool);

            return this;
        }
        
        public MonsterBuilder SetRangeAttack(float damage, float cooldown, IObjectPool objectPool)
        {
            EnemyAttackRange attackRange = _monster.GetComponent<EnemyAttackRange>();
            attackRange.Damage = damage;
            attackRange.Cooldown = cooldown;
            attackRange.Construct(objectPool);

            return this;
        }

        public MonsterBuilder SetPlayerCheckingDistances(float distanceForward, float distanceBack)
        {
            EnemyPlayerChecking playerChecking = _monster.GetComponent<EnemyPlayerChecking>();
            playerChecking.DistanceForward = distanceForward;
            playerChecking.DistanceBack = distanceBack;

            return this;
        }
        
    }
}