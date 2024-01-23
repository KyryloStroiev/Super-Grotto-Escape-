using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _following;

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }
    }
}