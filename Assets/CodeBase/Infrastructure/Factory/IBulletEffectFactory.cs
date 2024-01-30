using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IBulletEffectFactory
    {
        Task<GameObject> CreateBullet(string addressPrefab);
    }
}