using UnityEngine;
using System.Collections;

public class IslandInit : MonoBehaviour {

	public Transform islandStart;
	public EnemyScript enemyTrigger;

	// Use this for initialization
	void Start () {
		transform.position = islandStart.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyTrigger == null) {
			gameObject.GetComponent<TransformGeometry> ().Trigger ();
		} else if (enemyTrigger.Health < enemyTrigger.MaxHealth * 0.5) {
			gameObject.GetComponent<TransformGeometry> ().Trigger ();
		}
	}
}
