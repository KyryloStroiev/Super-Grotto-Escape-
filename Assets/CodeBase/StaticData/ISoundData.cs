using System;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    public interface ISoundData
    {
        AudioClip GetSound(Enum soundType);
    }
}