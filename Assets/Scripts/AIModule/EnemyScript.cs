/* enemy script
 * Place on a gameobject that represents an enemy
 * 
 */

using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	[SerializeField] private GameObject rigPrefab;
	private Animator rigAnimation;

	[SerializeField] private float maxHealth;
	[SerializeField] private float health;
	[SerializeField] private GameObject healthBar;
	
	[SerializeField] private GameObject expPrefab;
	[SerializeField] private GameObject voidPrefab;
	[SerializeField] private GameObject imagiPrefab;
	[SerializeField] private GameObject logioPrefab;
	
	[SerializeField] private bool canSplit = false;
	[SerializeField] private bool dyingAni = false;
	private bool dyingAudio = false;
	private bool doneDyingAudio = false;
	[SerializeField] private float dyingRotation = 400;
	[SerializeField] private GameObject splitIntoEnemyPrefab;
	
	[SerializeField] private float expDrop = 10;
	
	private AudioSource audioSource;
	[SerializeField] private AudioClip[] audioClips;
	
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
				Vector3 pos = transform.position;
				pos.x -= 0.5f;
				Instantiate(splitIntoEnemyPrefab, pos, Quaternion.identity);
				pos.x += 1f;
				Instantiate(splitIntoEnemyPrefab, pos, Quaternion.identity);
				
				
				if(EnemyCounterSingleton.Instance != null)
					EnemyCounterSingleton.Instance.CurrentEnemyCount--;
				
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
		
		// increment counter
		if(EnemyCounterSingleton.Instance != null)
		{
			EnemyCounterSingleton.Instance.CurrentEnemyCount++;
		}else Debug.Log("IzNull");
		
		if (GetComponent<AudioSource>())
		{
			audioSource = GetComponent<AudioSource>();
		}
		else
		{
			Debug.Log("No audio source found on object");
		}

		// animation
		if(!rigPrefab)
		{
			Debug.Log ("Rig Prefab not set");
		}
		
		rigAnimation = rigPrefab.GetComponent<Animator>();
		if(!rigAnimation)
		{
			Debug.Log ("Rig Animator not set");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//If health is zero, be destroyed.
		if (Health <= 0.0f) 
		{
			if(!dyingAudio)
				StartCoroutine(DoDeathAudio());
			
			if(!DoDeathAnimation() && doneDyingAudio)
				FinishDying();
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

		if(audioSource != null && audioSource.enabled)
		{
			int audioSampleNum = (int)Random.Range(0,audioClips.Length);
			//audioSource.clip.Equals();
			audioSource.PlayOneShot(audioClips[audioSampleNum]);
		}
		// play animation
		rigAnimation.SetTrigger( "Hurt" );
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
	
	private bool DoDeathAnimation(){
		if(gameObject.transform.FindChild("Healthbar") != null) 
		{
			Destroy (gameObject.transform.FindChild("Healthbar").gameObject);
			gameObject.GetComponent<NavMeshAgent>().enabled = false;
			gameObject.GetComponent<EnemyBehaviour>().enabled = false;
		}
		
		if(gameObject.GetComponent<ShrinkToOblivion>() != null)
		{
			gameObject.GetComponent<ShrinkToOblivion>().trigger = transform.localScale != Vector3.zero;
			gameObject.transform.Rotate(Vector3.up * dyingRotation * Time.deltaTime);
			dyingRotation *= 1.075f;
			dyingAni = transform.localScale != Vector3.zero;
		}
		else
		{
			dyingAni = false;
		}
		
		return dyingAni;
	}
	
	private IEnumerator DoDeathAudio()
	{
		dyingAudio = true;
		audioSource.PlayOneShot(audioClips[0]);
		yield return new WaitForSeconds(audioClips[0].length);
		doneDyingAudio = true;
		yield return null;
	}
	
	private void FinishDying(){
		PlayerStats stats = (GameObject.FindGameObjectWithTag("Player")).GetComponent<PlayerStats>();
		Vector3 pos = transform.position;
		pos.y += 0.5f;
		PickupOnTrigger obj;
		
		// Create ParticleDrop
		switch (ActivePowerManager.instance.CurrentPower)
		{
		case ActivePower.Imagi:
			obj = (Instantiate(imagiPrefab, pos, Quaternion.identity) as GameObject).GetComponent<PickupOnTrigger>();
			obj.StatModifyValue = stats.MaxImagi * 0.10f;
			break;
		case ActivePower.Void:
			obj = (Instantiate(voidPrefab, pos, Quaternion.identity) as GameObject).GetComponent<PickupOnTrigger>();
			obj.StatModifyValue = stats.MaxVoid * 0.10f;
			break;
		case ActivePower.Logio:
			obj = (Instantiate(logioPrefab, pos, Quaternion.identity) as GameObject).GetComponent<PickupOnTrigger>();
			obj.StatModifyValue = stats.MaxLogio * 0.10f;
			break;
		}
		
		// Create EXP
		pos.x += 0.5f;
		obj = (Instantiate(expPrefab, pos, Quaternion.identity) as GameObject).GetComponent<PickupOnTrigger>();
		obj.StatModifyValue = expDrop;
		
		// decrement the counter
		if (EnemyCounterSingleton.Instance != null)
		{
			EnemyCounterSingleton.Instance.CurrentEnemyCount--;
		}
		Destroy(gameObject);
	}
}
