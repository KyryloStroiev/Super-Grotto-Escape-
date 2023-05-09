using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    
    public float health;
    public GameObject explosenEffect;
	
    public void TakeDamage(float damage)
    {

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(explosenEffect, transform.position, Quaternion.identity);
        GlobalEventManager.SendEnemyKilled();
        Destroy(gameObject);
    }


}
