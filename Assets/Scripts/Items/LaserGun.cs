using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    private Animator animator;
	public GameObject laserPrefab;
	private bool isLaserActive = false;
	private void Start()
	{
		animator = GetComponent<Animator>();
		if (laserPrefab != null)
		{
			laserPrefab.SetActive(false);
		}
		
	}

	private void Update()
	{
		if(!isLaserActive)
			StartCoroutine(ShotLaser());

	}

	IEnumerator ShotLaser()
	{
		isLaserActive=true;
		animator.SetBool("ShotLaser", true);
		yield return new WaitForSeconds(1.5f);
		laserPrefab.SetActive(true);
		yield return new WaitForSeconds (2);
		animator.SetBool("ShotLaser", false);
		laserPrefab.SetActive(false);
		yield return new WaitForSeconds(3);
		isLaserActive = false;
	}
}

