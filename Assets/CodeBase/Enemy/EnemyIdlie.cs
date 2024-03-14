using CodeBase.Enemy.EnemyState;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyIdlie : MonoBehaviour, IEnemyState
    {
        public void Enable()
        {
            
        }

        public void Disable()
        {
           
        }
        
        public bool IsEnable { get; set; }
    }
}