using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyLogic : MonoBehaviour
{
    
    public float health;
    public GameObject explosenEffect;
    [SerializeField] private int points;

    private GameManager gameManager;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	public void TakeDamage(float damage)
    {

        health -= damage;
        if (health <= 0)
        {
            gameManager.Scoring(points);
            Die();
        }
    }

    void Die()
    {
        Instantiate(explosenEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
