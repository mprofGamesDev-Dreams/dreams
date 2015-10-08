using UnityEngine;
using System.Collections;

public class FinalMovingPlatformScene : MonoBehaviour 
{
	[SerializeField] private Transform dreamer;
	[SerializeField] private float transitionTime;
	
	private InputHandler playerInput;
	//private Transform cameraTransform;
	
	private bool triggerEvent = true; public bool TriggerEvent { set { triggerEvent = true; } }
	
	OnTriggerPlay audioEvent;
	GameObject player;
	
	bool rotateControl = false;

	Transform mainCamera;
	private void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		
		playerInput = player.GetComponent<InputHandler>();
		//cameraTransform = player.GetComponent<Transform>();
		//cameraTransform = player.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
		audioEvent = GetComponent<OnTriggerPlay>();
	}
	
	private Transform SceneCamera;
	
	private void Update () 
	{
		
		
		
		if( audioEvent.CurrentState != EAudioState.isWaiting && triggerEvent )
		{
			playerInput.ControllerConstraints = EControlConstraints.DisableAll;
			
			triggerEvent = false;
			
			rotateControl = true;
			mainCamera = Camera.main.transform;
		}
		
		if( rotateControl )
		{
			if(SceneCamera == null)
				SceneCamera = TurnOnSceneCamera(true);
			
			CameraAction ();
		}
		
		if( audioEvent.CurrentState == EAudioState.isFinished )
		{
			rotateControl = false;
			if(Quaternion.LookRotation(SceneCamera.forward) != Quaternion.LookRotation(mainCamera.forward))
			{
				SceneCamera.rotation = Quaternion.Lerp( SceneCamera.rotation, mainCamera.rotation, Time.deltaTime * 3 );
				return;
			}
			
			playerInput.ControllerConstraints = EControlConstraints.EnableAll;
			
			TurnOnSceneCamera(false);

			Destroy(this);
		}
	}
	
	
	private void CameraAction()
	{
		Quaternion rot = Quaternion.LookRotation( dreamer.position - SceneCamera.position );
		
		// Vectors Not Similar Enough Then They Will Lerp Instead Of Fix On Position
		if(Vector3.Dot( SceneCamera.forward, rot * Vector3.forward  ) <= 0.99)
		{
			SceneCamera.rotation = Quaternion.Lerp( SceneCamera.rotation, rot, Time.deltaTime * 3 );
			return;
		}
		
		SceneCamera.LookAt(dreamer.position);
	}
	
	// Always Returns SceneCamera
	private Transform TurnOnSceneCamera( bool state )
	{
		Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		Camera sceneCamera;
		
		// Make Sure SceneCamera Exists If Not Create It
		GameObject obj;
		if( (obj = GameObject.FindGameObjectWithTag("SceneCamera")) == null )
			sceneCamera = CreateSceneCamera();
		else sceneCamera = obj.GetComponent<Camera>();
		
		if( sceneCamera == null )
			sceneCamera = CreateSceneCamera();
		
		mainCamera.enabled = !state;
		sceneCamera.enabled = state;
		
		return sceneCamera.transform;
	}
	
	private Camera CreateSceneCamera()
	{
		GameObject camObj = new GameObject("SceneCamera", typeof(Camera));
		Transform sceneCamera = camObj.GetComponent<Transform>();
		sceneCamera.tag = "SceneCamera";
		sceneCamera.position = Camera.main.transform.position;
		sceneCamera.rotation = Camera.main.transform.rotation;
		sceneCamera.parent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		
		sceneCamera.GetComponent<Camera>().cullingMask = Camera.main.cullingMask;
		
		return sceneCamera.GetComponent<Camera>();
	}
}
