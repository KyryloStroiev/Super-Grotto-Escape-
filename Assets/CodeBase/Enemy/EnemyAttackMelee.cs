using System;
using System.Linq;
using CodeBase.Logic;
using CodeBase.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttackMelee : MonoBehaviour
    {
        public float Damage { get; set; }
        public float Cooldown { get; set; }
        
        public Transform AttackPoint;

        public EnemySoundType AttackSound;
        
        private float _cleavage = 0.5f;

        private const string LayerName = "Player";
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;

        private EnemyAnimator _animator;
        private EnemySound _sound;
        private Collider2D[] _hits = new Collider2D[1];

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(LayerName);
            _animator = GetComponent<EnemyAnimator>();
            _sound = GetComponent<EnemySound>();
        }

        private void Update() =>
            UpdateCooldown();

        public void StartAttack()
        {
            if (CanAttack())
            {
                _animator.PlayAttack();
               
                _isAttacking = true;
            }
        }

        private void OnAttack()
        {
            if (Hit(out Collider2D hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
                _sound.PlayOneShot(AttackSound);
                PhysicsDebug.DrawDebug(AttackPoint.position, _cleavage, 2);
            }
        }

        private bool Hit(out Collider2D hit)
        {
            int hitAmount = Physics2D.OverlapCircleNonAlloc(AttackPoint.position, _cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            return hitAmount > 0;
        }

        private void OnAttackEnded()
        {
            _attackCooldown = Cooldown;

            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() =>
            _attackCooldown <= 0;
    }
}