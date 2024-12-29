using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using Sirenix.Utilities;
using UnityEngine;

namespace CodeBase.Items
{
    public class Gate : MonoBehaviour
    {
        private static readonly int OpenHash = Animator.StringToHash("Open");
        private int numberButtonsPressed = 0;

        private List<ButtonGate> _buttos = new();
        public Animator _animator;
        private bool isGateOpen;

        private void Awake() => 
            FindAllButton();

        private void PressButton(int number)
        {
            numberButtonsPressed += number;
            if (numberButtonsPressed >= _buttos.Count)
            {
                OpenGate();
            }
        }

        private void OpenGate()
        {
            if (!isGateOpen)
            {
                _animator.SetTrigger(OpenHash);
                isGateOpen = true;
                numberButtonsPressed = _buttos.Count;
            }
        }

        private void FindAllButton()
        {
            ButtonGate[] buttonsArray = GetComponentsInChildren<ButtonGate>();
            _buttos.AddRange(buttonsArray);

            foreach (ButtonGate button in _buttos)
            {
                button.pressButton += PressButton;
            }
        }

        private void OnDestroy()
        {
            foreach (ButtonGate button in _buttos)
            {
                button.pressButton -= PressButton;
            }
        }

      
    }
}