using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int AggroMove = Animator.StringToHash("AggroMove");
        private static readonly int Attack = Animator.StringToHash("Attack");

        private Animator _animator;
        private EnemyMove _move;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _move = GetComponent<EnemyMove>();
        }

        public void PlayMove(bool isMove) =>
            _animator.SetBool(Move, isMove);

        public void PlayAggroMove(bool isMove) =>
            _animator.SetBool(AggroMove, isMove);
        
        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack);
    }
}