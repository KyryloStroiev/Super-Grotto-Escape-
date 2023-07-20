
using UnityEngine;
using Cinemachine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
	private PlayerMovement playerMovement;
	private float offSetY = 2.0f;

	[Inject]
	private void Construct(PlayerMovement playerMovement)
	{
		this.playerMovement = playerMovement;
	}
	void Start()
    {
		camera = GetComponent<CinemachineVirtualCamera>();
		if (playerMovement != null)
		{
			camera.Follow = playerMovement.transform;
		}
	
	}


	void Update()
	{
		Vector3 trackedObjectOffset = camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset;

		if (playerMovement.isLookingUp)
		{
			trackedObjectOffset.y = offSetY;
		}
		else if (playerMovement.moveDirection.y < 0)
		{
			trackedObjectOffset.y = -offSetY;
		}
		else
		{
			trackedObjectOffset.y = 0;
		}

		camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = trackedObjectOffset;
	}
}
