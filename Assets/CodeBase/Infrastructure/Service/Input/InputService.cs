using System;
using CodeBase.Infrastructure.Service.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Service
{
    public class InputService : IInputService
    {
        private PlayerInput _input;
        public event Action Shoot;
        public event Action Jump;


        public InputService()
        {
            _input = new PlayerInput();
            _input?.Enable();
            _input.Player.Jump.performed +=_=> Jump?.Invoke();
            _input.Player.Shoot.performed += _ => Shoot?.Invoke();
        }
        
        public Vector2 Axis => 
            _input.Player.Move.ReadValue<Vector2>();
        
        

       

        private void OnDisable()
        {
            _input?.Disable();
            _input.Player.Jump.performed -=_=> Jump?.Invoke();
            _input.Player.Shoot.performed -= _ => Shoot?.Invoke();
        }
    }
}