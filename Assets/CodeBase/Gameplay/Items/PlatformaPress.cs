using System;
using UnityEngine;

public class PlatformaPress : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxPlatforma;
    public Animator _animator;
    private readonly static int Enter = Animator.StringToHash("Enter");
    public event Action<bool> OnPressed;
    
    private const string LayerPlayer = "Player";
    private bool _isPressed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(LayerPlayer))
        {
            PressPlatform();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            ReleasePlatform();
    }

    private void PressPlatform()
    {
        PlayAnimation(true);
       
    }

    private void ReleasePlatform() => 
        PlayAnimation(false);

    private void PlayAnimation(bool play)
    {
        _animator.SetBool(Enter, play);
        OnPressed?.Invoke(play);
    }

    public void DeleteTriggerBox()
    {
        if (_boxPlatforma != null)
        {
            _boxPlatforma.enabled = false;
        }
    }
}
