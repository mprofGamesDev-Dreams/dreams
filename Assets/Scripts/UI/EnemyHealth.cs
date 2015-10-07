using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	[SerializeField] private GameObject Enemey;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Enemey.GetComponent<EnemyScript>().Health <= 0)
        {
            Destroy(gameObject);
        }

		gameObject.transform.localScale = new Vector3 ((Enemey.GetComponent<EnemyScript> ().Health / Enemey.GetComponent<EnemyScript> ().MaxHealth),
		                                               gameObject.transform.localScale.y,
		                                               gameObject.transform.localScale.z);
	}
}
