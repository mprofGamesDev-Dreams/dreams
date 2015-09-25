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

	[SerializeField] private bool canSplit = false;
	[SerializeField] private GameObject splitIntoEnemyPrefab;

	[SerializeField] private float expDrop = 10;

	public float Health 
	{
		get
		{
			return health;
		}
		set 
		{
			if(canSplit)
			{
				Instantiate(splitIntoEnemyPrefab, transform.position, Quaternion.identity);
				Instantiate(splitIntoEnemyPrefab, transform.position, Quaternion.identity);

				Destroy(this.gameObject);
			}

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
		health = MaxHealth;

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
			PlayerStats stats = (GameObject.FindGameObjectWithTag("Player")).GetComponent<PlayerStats>();
			Vector3 pos = transform.position;
			pos.y += 0.5f;
			PickupOnTrigger obj;

			// Create ParticleDrop
			switch (ActivePowerUI.instance.CurrentPower)
			{
			case ActivePower.Imagi:
				obj = (Instantiate(imagiPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
				obj.StatModifyValue = stats.MaxImagi * 0.10f;
				break;
			case ActivePower.Void:
				obj = (Instantiate(voidPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
				obj.StatModifyValue = stats.MaxVoid * 0.10f;
				break;
			case ActivePower.Logio:
				obj = (Instantiate(logioPrefab, pos, Quaternion.identity ) as GameObject).GetComponent<PickupOnTrigger>();
				obj.StatModifyValue = stats.MaxLogio * 0.10f;
				break;
			}

			// Create EXP
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
			if (!healthBar.activeInHierarchy)
			{
				healthBar.SetActive(true);
			}
		}
	}
}
