using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class ExitDoor : MonoBehaviour
    {
        private const string LayerName = "Player";
        private const string Opens = "Open";
        public DialogueTrigger _dialogueTrigger;
        public GameObject GameOver;

        private bool isOpen = false;
        private Animator _animator;

        private void Awake()
        {
            _dialogueTrigger = GetComponentInParent<DialogueTrigger>();
            _animator = GetComponent<Animator>();
            _dialogueTrigger.OpenDoor += Open;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(LayerName))
            {
                Instantiate(GameOver);
                Time.timeScale = 0f;
            }
            
        }

        private void Open()
        {
            isOpen = true;
            _animator.SetTrigger(Opens);
        }

    }
}