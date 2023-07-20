using System;
using System.Collections;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
	
	public float MaxHealth;
    [HideInInspector] public bool canTakeDamage = true;
	private float damageDelay = 1.5f;
	private float _health;
	public  event Action<float> OnHealthChange;/// 
	public event Action GameOver;

	private SpriteRenderer spriteRenderer;
    private PlayerAnimator playerAnimator;
	public float Health
	{
		get { return _health; }
		set
		{
			_health = Mathf.Clamp(value, 0, MaxHealth);
			if(_health <= 0 )
			{
				_health = 0;
				GameOver?.Invoke();
			}
		}
	}

	
    private void Start()
	{	
		Health = MaxHealth;
		playerAnimator = GetComponent<PlayerAnimator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void TakeDamage( float damage)
    {

		playerAnimator.TakeDamage();
		StartCoroutine(TakeDamageDelay());
		Debug.Log("DaMAGE");
		Health -= damage;
		OnHealthChange?.Invoke(Health);
	}

	public void Heal(float healing)
    {
        Health += healing;
    }

	IEnumerator TakeDamageDelay()
	{
		canTakeDamage = false;
		spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
		yield return new WaitForSeconds(damageDelay);
		canTakeDamage = true;
		spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
}
