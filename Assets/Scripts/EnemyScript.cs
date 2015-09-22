/* enemy script
 * Place on a gameobject that represents an enemy
 * 
 */

using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	[SerializeField] private float maxHealth;
	[SerializeField] private float health;
	[SerializeField] private GameObject healthBar;
	public float Health 
	{
		get
		{
			return health;
		}
		set 
		{
			health = value;
		}
	}

	public float MaxHealth 
	{
		get
		{
			return maxHealth;
		}
		set 
		{
			maxHealth = value;
		}
	}

	// Use this for initialization
	void Start () {
		//MaxHealth = 4;
		Health = MaxHealth;

		if (healthBar) 
		{
			healthBar.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//If health is zero, be destroyed.
		if (Health <= 0.0f) {
			Destroy(gameObject);
		}
	
	}

	//Take a special attack, lose two health
	public void TakeSA(){
		Health = Health - 2.0f;
		ActivateHealthBar ();
	}

	public void TakeMA(){
		Health = Health - 1.0f;
		ActivateHealthBar ();
	}

	public void TakeDamage(float damage)
	{
		Health = Health - damage;
		ActivateHealthBar ();
	}

	private void ActivateHealthBar()
	{
		if (healthBar) 
		{
			if (!healthBar.active)
			{
				healthBar.SetActive(true);
			}
		}
	}
}
