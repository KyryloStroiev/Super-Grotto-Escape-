using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Player
{
    public class FlipDirectionPlayer : FlipDirection
    {
        private PlayerMovement _playerMovement;

        private void Awake() => 
            _playerMovement = GetComponent<PlayerMovement>();

        private void Update() => 
            CheckDirection(_playerMovement.HorizontalVelocity);
    }
}