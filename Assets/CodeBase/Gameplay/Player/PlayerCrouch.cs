using System;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.Input;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerCrouch : MonoBehaviour
    {
        public Transform ShotPoint;
        public bool IsCrouch { get; set; }

        private Vector3 _startShotPointPosition;
        private ColliderChecking _colliderChecking;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _colliderChecking = GetComponent<ColliderChecking>();
            _inputService = inputService;
        }

        private void Awake()
        {
            _startShotPointPosition = ShotPoint.localPosition;
        }

        private void Update() => 
            Crouch();


        private void Crouch()
        {
            if (!_colliderChecking.IsLadder)
            {
                if (_inputService.Axis.y < 0)
                    StartCrouch();
                else
                    StopCrouch();
            }
        }

        private void StartCrouch()
        {
            IsCrouch = true;
            ShotPoint.localPosition = new Vector3(ShotPoint.localPosition.x, -0.5f, ShotPoint.localPosition.z);
        }

        private void StopCrouch()
        {
            IsCrouch = false;
            ShotPoint.localPosition = _startShotPointPosition;
        }
    }
}