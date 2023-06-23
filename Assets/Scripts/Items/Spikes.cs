
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int delayTime;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Up", delayTime);
    }


  

}
