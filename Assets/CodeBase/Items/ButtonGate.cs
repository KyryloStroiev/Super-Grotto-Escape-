using System;
using UnityEngine;

namespace CodeBase.Items
{
    public class ButtonGate : MonoBehaviour
    {
        private static readonly int EnterHash = Animator.StringToHash("Enter");
        private static readonly int ExitHash = Animator.StringToHash("Exit");
        
        public Animator _animator;
        private bool isPressButton;
        
        public event Action<int> pressButton; 
        private void OnTriggerEnter2D(Collider2D other)
        {
            _animator.SetTrigger(EnterHash);
            pressButton?.Invoke(1);
            isPressButton = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _animator.SetTrigger(ExitHash);
            if (isPressButton)
            {
                pressButton?.Invoke(-1);
                isPressButton = false;
            }
         
        }
    }
}
