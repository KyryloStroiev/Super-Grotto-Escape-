using UnityEngine;

namespace CodeBase.Logic
{
    public class Explosion : MonoBehaviour
    {
        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
