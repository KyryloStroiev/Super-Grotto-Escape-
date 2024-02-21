using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Player;
using UnityEngine;
using Zenject;
using IPoolable = CodeBase.Logic.IPoolable;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Service.ObjectPool
{
    public class ObjectPool : IObjectPool
    {
        private Dictionary<string, Queue<GameObject>> _objectQueues = new();

        private int _size = 7;
        private const string ObjectPoolFolder = "ObjectPool";

        private  IBulletEffectFactory _bulletEffectFactory;
        private IAssetProvider _assetProvider;
        private GameObject _poolObject;


        public async Task Instantiate(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _bulletEffectFactory = new BulletEffectFactory(_assetProvider);
            CreateFolder();
            await Create(AssetsAdress.PlayerBullet);
            await Create(AssetsAdress.EnemyBullet);
            await Create(AssetsAdress.BulletEffect);
            await Create(AssetsAdress.ExplosionSmall);
            await Create(AssetsAdress.GunBullet);
        }


        private async Task Create(string objectAdress)
        {
            _objectQueues[objectAdress] = new Queue<GameObject>();
            
            for (int i = 0; i < _size; i++)
            {
                GameObject obj = await _bulletEffectFactory.CreateBullet(objectAdress);
                _objectQueues[objectAdress].Enqueue(obj);
                obj.GetComponent<IPoolable>().Construct(this, objectAdress);
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