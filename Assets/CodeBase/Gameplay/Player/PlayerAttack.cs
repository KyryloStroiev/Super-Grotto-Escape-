using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public float Damage { get; set; }
        
        public Transform shootPoint;
        
        private IInputService _inputService;
        private FlipDirectionPlayer _flip;
        private PlayerAnimator _animator;
        private IObjectPool _objectPool;
        private PlayerSounds _playerSounds;
        
        public void Construct(IInputService inputService, IObjectPool objectPool)
        {
            _inputService = inputService;
            _objectPool = objectPool;
            _inputService.Shoot += StartAttack;
            _flip = GetComponent<FlipDirectionPlayer>();
            _animator = GetComponent<PlayerAnimator>();
            _playerSounds = GetComponent<PlayerSounds>();

        }
        private void StartAttack() => 
            _animator.PlayAttack();

        private void OnAttack()
        {
            Vector2 direction = _flip.IsFacingRight ? Vector2.right : Vector2.left;
            _playerSounds.PlayOneSound(PlayerSoundType.Shot);
            GameObject bullet = _objectPool.GetPooledObject(AssetsAdress.PlayerBullet, shootPoint.position);
            bullet.GetComponent<Bullet>().StartShoot(direction, Damage);
           
        }

        private void OnDisable() => 
            _inputService.Shoot -= StartAttack;
        
    }
}