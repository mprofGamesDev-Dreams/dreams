using UnityEngine;
using System.Collections;

public class Cooldown : MonoBehaviour {
	[SerializeField] private GameObject player;
	[SerializeField] private ActivePower powerType;
	// Use this for initialization
	void Start () {
		gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x,
		                                               (0),
		                                               gameObject.transform.localScale.z);
	}

	
	// Update is called once per frame
	void Update () {
		switch (powerType) {
		case ActivePower.Imagi:
			if (player.GetComponent<AbilityBehaviours> ().ImagiTimer > 0) {
				gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x,
				                                               (1 - (player.GetComponent<AbilityBehaviours> ().ImagiTimer / player.GetComponent<AbilityBehaviours> ().ImagiCD)),
				                                               gameObject.transform.localScale.z);
			}
			break;
		case ActivePower.Logio:
			if (player.GetComponent<AbilityBehaviours> ().LogioTimer > 0) {
				gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x,
				                                               (1 - (player.GetComponent<AbilityBehaviours> ().LogioTimer / player.GetComponent<AbilityBehaviours> ().LogioCD)),
				                                               gameObject.transform.localScale.z);
			}
			break;
		case ActivePower.Void:
			if (player.GetComponent<AbilityBehaviours> ().VoidTimer > 0) {
				gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x,
				                                               (1 - (player.GetComponent<AbilityBehaviours> ().VoidTimer / player.GetComponent<AbilityBehaviours> ().VoidCD)),
				                                               gameObject.transform.localScale.z);
			}
			break;
		}
	}
}
