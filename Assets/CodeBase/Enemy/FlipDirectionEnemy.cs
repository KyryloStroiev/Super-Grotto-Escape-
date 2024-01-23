﻿using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class FlipDirectionEnemy : FlipDirection
    {
        private EnemyMove _move;

        private void Awake() => 
            _move = GetComponent<EnemyMove>();

        private void Update() => 
            CheckDirection(_move.HorizontalVelocity);
    }
}