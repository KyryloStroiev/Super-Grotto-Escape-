
using UnityEngine;
using Cinemachine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
	private CharacterController2D characterController;
	private float offSetY = 2.0f;

	[Inject]
	private void Construct(CharacterController2D characterController)
	{
		this.characterController = characterController;
	}
	void Start()
    {
		camera = GetComponent<CinemachineVirtualCamera>();
		camera.Follow = characterController.transform;
	}

    
    void Update()
    {
		Vector3 trackedObjectOffset = camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset;

		if (characterController.isLookingUp) 
		{
			trackedObjectOffset.y = offSetY;
		}
		else if(characterController.moveDirection.y < 0)
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
