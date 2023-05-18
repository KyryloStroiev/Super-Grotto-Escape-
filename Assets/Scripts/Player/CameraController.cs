using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera _camera;
	private CharacterController2D _characterController;
	private float offsetY = 2.0f;
	void Start()
    {
		_camera = FindObjectOfType<CinemachineVirtualCamera>();
		_characterController = GetComponent<CharacterController2D>();
	}

    
    void Update()
    {
		Vector3 trackedObjectOffset = _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset;

		if (_characterController.isLookingUp) 
		{
			trackedObjectOffset.y = offsetY;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			trackedObjectOffset.y = -offsetY;
		}
		 else
        {
            trackedObjectOffset.y = 0;
        }

       _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = trackedObjectOffset;
	}
}
