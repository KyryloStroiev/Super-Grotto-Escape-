using System;
using Cinemachine;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _following;
        public CinemachineVirtualCamera _virtualCamera;

        
        public void Follow(GameObject following)
        {
            if (following != null)
                _virtualCamera.Follow = following.transform;
            else
                Debug.LogError("The object to follow is null!");
        }
    }
}