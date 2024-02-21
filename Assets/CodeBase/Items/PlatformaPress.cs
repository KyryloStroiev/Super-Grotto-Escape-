using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformaPress : MonoBehaviour
{
    public Animator _animator;
    private readonly static int Enter = Animator.StringToHash("Enter");
    public event Action<bool> OnPressed;
    
    private void OnTriggerEnter2D(Collider2D other) => 
        PressPlatform();

    private void OnTriggerExit2D(Collider2D other) => 
        ReleasePlatform();

    private void PressPlatform() => 
        PlayAnimation(true);

    private void ReleasePlatform() => 
        PlayAnimation(false);

    private void PlayAnimation(bool play)
    {
        _animator.SetBool(Enter, play);
        OnPressed?.Invoke(play);
    }
}
