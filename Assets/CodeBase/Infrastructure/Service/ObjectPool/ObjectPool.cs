using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Player;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Service.ObjectPool
{
    public class ObjectPool : IObjectPool
    {
        private Dictionary<string, Queue<GameObject>> _objectQueues = new();

        private int _size = 14;
        private const string ObjectPoolFolder = "ObjectPool";

        private  IBulletEffectFactory _bulletEffectFactory;
        private GameObject _poolObject;

        [Inject]
        public ObjectPool(IBulletEffectFactory bulletEffectFactory)
        {
            _bulletEffectFactory = bulletEffectFactory;
        }
        
        public async Task Instantiate()
        {
            CreateFolder();
            await Create(AssetsAdress.PlayerBullet);
            await Create(AssetsAdress.EnemyBullet);
        }

        private async Task Create(string objectAdress)
        {
            _objectQueues[objectAdress] = new Queue<GameObject>();
            
            for (int i = 0; i < _size; i++)
            {
                GameObject obj = await _bulletEffectFactory.CreateBullet(objectAdress);
                _objectQueues[objectAdress].Enqueue(obj);
                obj.transform.SetParent(_poolObject.transform);
                obj.SetActive(false);
            }
        }

        public GameObject GetPooledObject(string objectAddress, Vector3 position)
        {
            if (_objectQueues.ContainsKey(objectAddress))
            {
                if (_objectQueues[objectAddress].Count > 0)
                {
                    GameObject obj = _objectQueues[objectAddress].Dequeue();
                    obj.transform.position = position;
                    obj.SetActive(true);
                    return obj;
                }
                else
                {
                    Create(objectAddress);
                }
                
            }
            return null;
        }

        public void ReturnToPool(GameObject bullet, string objectAddress)
        {
            bullet.SetActive(false);
            _objectQueues[objectAddress].Enqueue(bullet);
        }

        private void CreateFolder()
        {
            if(_poolObject == null)
                _poolObject = new GameObject(ObjectPoolFolder);
        }
    }

}