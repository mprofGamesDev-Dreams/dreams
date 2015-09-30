/*
 * Script that implements bullet logic.
 * Should be applied to bullet object that contains a rigidbody.
 * Speed and range of bullet should be defined.
 * Bullets can be assigned a specific power type.
 * 
 * Edit OnCollisionEnter to add aditional effects or Hit<TYPE> function for type specific effects
 */

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField] private ActivePower bulletType; public ActivePower BulletType { get {return bulletType;} }
	[SerializeField] private float speed;
	[SerializeField] private float range;
	[SerializeField] private float damage;

	private GameObject parentPlayer;

	// Use this for initialization
	void Start () {

		Vector3 bulletSpeed = new Vector3(parentPlayer.GetComponent<PlayerStats> ().DeltaPosition.x,
		                                  parentPlayer.GetComponent<PlayerStats> ().DeltaPosition.y,
										  parentPlayer.GetComponent<PlayerStats> ().DeltaPosition.z);
		//Debug.Log ( Vector3.Dot( parentPlayer.GetComponent<PlayerStats> ().DeltaPosition.normalized, transform.position ) );
		bulletSpeed = gameObject.transform.InverseTransformDirection (bulletSpeed);
		//bulletSpeed.z = bulletSpeed.z + speed;
		//gameObject.GetComponent<Rigidbody> ().velocity = gameObject.transform.TransformDirection( bulletSpeed);*/

		gameObject.GetComponent<Rigidbody> ().velocity = transform.forward * (speed + bulletSpeed.magnitude) ;
		Destroy (gameObject, range/gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Shield>()) //hit a shield.	
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
			switch(bulletType)
			{
			case ActivePower.Logio:
				HitLogio(collision);
				break;
			case ActivePower.Imagi:
				HitImagi(collision);
				break;
			case ActivePower.Void:
				HitVoid(collision);
				break;
			}

		} 
		else //hit terrain
		{

		}
		Destroy(gameObject);
	}

	private void HitLogio(Collision collision)
	{
		collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
	}

	private void HitImagi(Collision collision)
	{
		collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
		parentPlayer.GetComponent<PlayerStats> ().ImagiHit ();
	}

	private void HitVoid(Collision collision)
	{
		collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
		parentPlayer.GetComponent<PlayerStats> ().VoidHit ();
	}

	public void SetParent(GameObject player)
	{
		parentPlayer = player;
	}

}
