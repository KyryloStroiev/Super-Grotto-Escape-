
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bullet;
    private const string BulletGun = "BulletGun";

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
        ObjectPooler.Instance.SpawnFromPool(BulletGun, shotPoint.position, Quaternion.identity);
    }
}
