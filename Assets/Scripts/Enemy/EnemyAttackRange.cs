
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
	[SerializeField] private Transform shotPoints;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	public void Shot()
	{
		animator.SetBool("isShot", true);
	}

	public void ShotBullet()
	{
		ObjectPooler.Instance.SpawnFromPool("Fireball", shotPoints.position, Quaternion.identity);
		animator.SetBool("isShot", false);
	}
}
