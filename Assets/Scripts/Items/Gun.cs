using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Animator animator;
    public Transform shotPoint;
    public GameObject bullet;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

	public void Shot()
    {
       
			animator.SetTrigger("Shot");
    }

    void ShotBullet()
    {
        Instantiate(bullet, shotPoint.position, Quaternion.identity);
    }
}
