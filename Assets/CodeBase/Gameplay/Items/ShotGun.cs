using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Items
{
    public class ShotGun : MonoBehaviour
    {
        [SerializeField] private float _shotСooldown = 2;
        [SerializeField] private Transform _shotPoint;
        private float _readyToShot = 0;

        private bool _isPressed = false;
    
        private PlatformaPress _pressPlatform;
        private IObjectPool _objectPool;
        private int _damage = 1;

        [Inject]
        public void Construct(IObjectPool objectPool) => 
            _objectPool = objectPool;

        private void Awake()
        {
            _pressPlatform = GetComponentInChildren<PlatformaPress>();
            _pressPlatform.OnPressed += SetPressedState;
        }

        private void Update()
        {
            if (_isPressed && Cooldown())
            {
                GameObject bullet =  _objectPool.GetPooledObject(AssetsAdress.GunBullet, _shotPoint.position);
                bullet.GetComponent<Bullet>().StartShoot(Vector2.right, _damage);
            }
        }

        private void SetPressedState(bool isPressed) => 
            _isPressed = isPressed;

        private bool Cooldown()
        {
            _readyToShot += Time.deltaTime;
            if (_readyToShot >= _shotСooldown)
            {
                _readyToShot = 0;
                return true;
            }
            return false;
        }
    }
}
