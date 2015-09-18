using UnityEngine;
using System.Collections;

/*
 * Script for displaying a texture to the GUI
 * 
 * ATTACHMENT: Add to the player controller, set the MapRenderTexture and set the key to use
 * 
 */

public class ShowMap : MonoBehaviour
{
	public Texture MapTexture;
	public KeyCode MapKey;

	public float MapResolution = 512;

	private bool Visible = false;
	private float HalfW;
	private float HalfH;

	private Vector2 MapTopLeftPosition;

	void Start()
	{
		HalfW = Screen.width / 2;
		HalfH = Screen.height / 2;
		
		MapTopLeftPosition.x = HalfW - (MapResolution / 2);
		MapTopLeftPosition.y = HalfH - (MapResolution / 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(MapKey))
		{
			if( Visible ) Visible = false;
			else Visible = true;
		}
	}

	void OnGUI()
	{
		if( !Visible )
			return;

		Graphics.DrawTexture(new Rect(MapTopLeftPosition.x, MapTopLeftPosition.y, MapResolution, MapResolution), MapTexture);
	}
}
