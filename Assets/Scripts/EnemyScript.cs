using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float health;

	// Use this for initialization
	void Start () {
		health = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//If health is zero, be destroyed.
		if (health == 0.0f) {
			Destroy(gameObject);
		}
	
	}

	//Take a special attack, lose two health
	public void TakeSA(){
		health = health - 2.0f;
	}

	public void TakeMA(){
		health = health - 1.0f;
	}

	public void TakeDamage(float damage)
	{
		health = health - damage;
	}
}
