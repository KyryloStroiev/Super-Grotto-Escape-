
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
   
	public void ReturntoPool()
	{
		ObjectPooler.Instance.ReturnToPool("BulletEffect", gameObject);
	}


}
