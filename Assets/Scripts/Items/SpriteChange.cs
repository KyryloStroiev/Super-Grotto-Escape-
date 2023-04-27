using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    public Sprite[] sprites;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
	public GameObject explosenEffect;
	void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentSpriteIndex];
        
    }

    public void Change()
    {
		currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
		spriteRenderer.sprite = sprites[currentSpriteIndex];

		if (currentSpriteIndex == sprites.Length - 1)
		{
			Instantiate(explosenEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
  
}
