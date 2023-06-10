using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlatform: MonoBehaviour
{
    private Animator animator;
    public UnityEvent Shot;
    private bool isPress = false;
    private Coroutine pressCoroutine;
    public float interdalShot = 2f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        animator.SetBool("isPress", isPress);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		isPress = true;
		pressCoroutine = StartCoroutine(InvokePress(interdalShot));
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		isPress = false;
		if (pressCoroutine != null)
		{
			StopCoroutine(pressCoroutine);
		}
	}
	IEnumerator InvokePress(float interval)
    {
        while (true)
        {
            Shot.Invoke();
            yield return new WaitForSeconds(interval);
        }
    }
}


