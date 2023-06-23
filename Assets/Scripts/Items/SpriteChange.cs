
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
	public GameObject explosenEffect;
	[SerializeField] private Sprite[] sprites;
    private int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
	private const string ExplosionSmall = "ExplosionSmall";
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
			ObjectPooler.Instance.SpawnFromPool("ExplosionSmall", transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
  
}
