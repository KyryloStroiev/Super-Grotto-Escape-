using System;
using Cinemachine;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _following;
        public CinemachineVirtualCamera _virtualCamera;
        private GameObject _player;
        private PlayerLookUpDown _playerLook;
        private CinemachineFramingTransposer _transposer;


        private void Awake()
        {
             _transposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        public void Construct(GameObject player)
        {
            _player = player;
            _playerLook = _player.GetComponent<PlayerLookUpDown>();
        }

        private void Update()
        {
            if (_playerLook != null)
            {
                float verticalInput = _playerLook.VerticalDirection();
                MoveCameraVertical(verticalInput);
            }
            
            
        }

        public void Follow()
        {
            if (_player != null)
                _virtualCamera.Follow = _player.transform;
            else
                Debug.LogError("The object to follow is null!");
        }
        
        private void MoveCameraVertical(float input)
        {
            _transposer.m_TrackedObjectOffset.y = input * 2;
        }
    }
}