using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.Input
{
    public interface IInputService
    {
        Vector2 Axis { get;}

        event Action Shoot;
        event Action Jump;
    }
}