/*
 * Script that implements bullet logic.
 * Should be applied to bullet object that contains a rigidbody.
 * Speed and range of bullet should be defined.
 * Bullets can be assigned a specific power type.
 * 
 * Edit OnCollisionEnter to add aditional effects
 */

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] private ActivePower bulletType;
	[SerializeField] private float speed;
	[SerializeField] private float range;
	// Use this for initialization
	void Start () {
		bulletType = ActivePower.Imagi;
		gameObject.GetComponent<Rigidbody> ().velocity = gameObject.transform.TransformDirection( new Vector3 (0, 0, speed));
		Destroy (gameObject, range/speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Shield>()) //hit a shield
		{
			Shield shieldScript = collision.gameObject.GetComponent<Shield>();

			switch(bulletType)
			{
			case ActivePower.Logio:
				shieldScript.ApplyDamage(Shield.shieldOptions.Logio, 10	);
				break;
			case ActivePower.Imagi:
				shieldScript.ApplyDamage(Shield.shieldOptions.Imagi, 10	);
				break;
			case ActivePower.Void:
				shieldScript.ApplyDamage(Shield.shieldOptions.Void, 10);
				break;
			}
		} 
		else if (collision.gameObject.GetComponent<EnemyScript>()) //hit a bad guy
		{
			collision.gameObject.GetComponent<EnemyScript>().TakeSA();
		} 
		else //hit terrain
		{

		}
		Destroy(gameObject);
	}
}
