using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	//EnemyScript gameobject
	EnemyScript ES;

	void Update() {

		//Ray origin is the mouse position. Chang to reticle position when implemented.
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Raycast hit object
		RaycastHit hit;

		//If Raycast hits anything, draw a line of sight for debug
		if (Physics.Raycast(ray, out hit, 100)) {
			Debug.DrawLine(ray.origin, hit.point, Color.red);

			//If Mouse1 is pressed, print special attack
			if (Input.GetMouseButtonDown(0)){
				Debug.Log("Special attack fired");
				//If the raycast hits an enemy, print that it is hit
				if(hit.collider.gameObject.name == "Enemy"){
					ES = (EnemyScript)hit.collider.gameObject.GetComponent (typeof(EnemyScript));
					ES.TakeSA();
					Debug.Log ("Enemy Hit");
				}
			}
		}

		//Smaller range melee attack
		if (Physics.Raycast (ray, out hit, 2)) {
			Debug.DrawLine(ray.origin, hit.point, Color.green);
			if (Input.GetMouseButtonDown(1)){
				Debug.Log("Melee Attack triggered");
				if(hit.collider.gameObject.name == "Enemy"){
					ES = (EnemyScript)hit.collider.gameObject.GetComponent (typeof(EnemyScript));
					ES.TakeMA();
					Debug.Log ("Enemy Hit");
				}
			}
		}
	}
}