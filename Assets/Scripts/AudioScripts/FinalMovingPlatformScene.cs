﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class FinalMovingPlatformScene : MonoBehaviour 
{
	[SerializeField] private Transform dreamer;
	[SerializeField] private float transitionTime;
	
	//private InputHandler playerInput;
	//private Transform cameraTransform;
	
	private bool triggerEvent = true; public bool TriggerEvent { set { triggerEvent = true; } }
	
	OnTriggerPlay audioEvent;
	GameObject player;
	
	bool rotateControl = false;

	Transform mainCamera;
	private void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		
		//playerInput = player.GetComponent<InputHandler>();
		//cameraTransform = player.GetComponent<Transform>();
		//cameraTransform = player.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
		audioEvent = GetComponent<OnTriggerPlay>();
	}
	
	private Transform SceneCamera;
	
	private void Update () 
	{
		if( audioEvent.CurrentState != EAudioState.isWaiting && triggerEvent )
		{
			triggerEvent = false;
			
			rotateControl = true;
			mainCamera = Camera.main.transform;

			player.GetComponent<FirstPersonController>().enabled = false;
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

				Vector3 dir = SceneCamera.forward;

			player.GetComponent<Transform>().forward = new Vector3( dir.x, 0, dir.z );
			mainCamera.forward = new Vector3( dir.x, dir.y, dir.z );

			player.GetComponent<FirstPersonController>().mouseLook.Init(player.GetComponent<Transform>(), mainCamera);

			//SceneCamera.rotation = Quaternion.Lerp( SceneCamera.rotation, mainCamera.rotation, Time.deltaTime * 3 );

			player.GetComponent<FirstPersonController>().enabled = true;

			TurnOnSceneCamera(false);

			Destroy(this);
		}
	}
	
	
	private void CameraAction()
	{
		Quaternion rot = Quaternion.LookRotation( dreamer.position - SceneCamera.position );
		
		// Vectors Not Similar Enough Then They Will Lerp Instead Of Fix On Position
		if(Vector3.Dot( SceneCamera.forward, rot * Vector3.forward  ) <= 1)
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

		BloomOptimized sceneBloom = camObj.AddComponent<BloomOptimized>();
		
		BloomOptimized mainBloom = Camera.main.GetComponent<BloomOptimized>();
		
		sceneBloom.threshold = mainBloom.threshold;
		sceneBloom.intensity = mainBloom.intensity;
		sceneBloom.blurSize = mainBloom.blurSize;
		sceneBloom.blurIterations = mainBloom.blurIterations;
		sceneBloom.blurType = mainBloom.blurType;
		sceneBloom.fastBloomShader = mainBloom.fastBloomShader;

		return sceneCamera.GetComponent<Camera>();
	}
}
