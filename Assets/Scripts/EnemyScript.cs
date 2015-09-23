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

	[SerializeField] private GameObject expPrefab;
	[SerializeField] private GameObject voidPrefab;
	[SerializeField] private GameObject imagiPrefab;
	[SerializeField] private GameObject logioPrefab;

	[SerializeField] private float expDrop = 10;
	[SerializeField] private float particleDrop = 10;

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
		if (Health <= 0.0f) 
		{
			Vector3 pos = transform.position;
			pos.y += 0.5f;
			PickupOnTrigger obj;

			switch (ActivePowerUI.instance.CurrentPower)
			{
			case ActivePower.Imagi:
				obj = (Instantiate(imagiPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
				obj.StatModifyValue = particleDrop;
				break;
			case ActivePower.Void:
				obj = (Instantiate(voidPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
				obj.StatModifyValue = particleDrop;
				break;
			case ActivePower.Logio:
				obj = (Instantiate(logioPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
				obj.StatModifyValue = particleDrop;
				break;
			}

			pos.x += 0.5f;
			obj = (Instantiate(expPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
			obj.StatModifyValue = expDrop;




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
