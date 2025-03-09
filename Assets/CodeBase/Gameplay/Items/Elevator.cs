using UnityEngine;

public class Elevator : MonoBehaviour
{
    private const string Active = "Active";
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _animator.SetTrigger(Active);
    }
}
