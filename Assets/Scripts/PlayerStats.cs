using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * Script for handling stats
 * 
 * ATTACHMENT: Add to the player object
 * 
 */

public class PlayerStats : MonoBehaviour
{
	// Access to the player object
	private GameObject Player;
	private FirstPersonController playerController;

	[Header("Health")]
	[SerializeField] private int startHealth = 100;
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private float healthRegen = 10;
	[SerializeField] private float currentHealth = 100;

	[Header("Stamina")]
	[SerializeField] private int startStamina = 100;
	[SerializeField] private int maxStamina = 100;
	[SerializeField] private int staminaRegen = 10;
	[SerializeField] private float currentStamina = 100;

	[Header("Void Stats")] // Void Has No Regen
	[SerializeField] private int startVoid = 100;
	[SerializeField] private int maxVoid = 100;
	[SerializeField] private float currentVoid = 100;
	
	[Header("Imagi Stats")]
	[SerializeField] private int startImagi = 100;
	[SerializeField] private int maxImagi = 100;
	[SerializeField] private int imagiRegen = 10;
	[SerializeField] private float currentImagi = 75;

	[Header("Logio Stats")]
	[SerializeField] private int startLogio = 100;
	[SerializeField] private int maxLogio = 100;
	[SerializeField] private int logioRegen = 10;

	[Header("Combat Variables")]
	[SerializeField] private int voidRegen = 10; // lifesteal
	[SerializeField] private float imagiBuff = 1.25f;
	[SerializeField] private int buffDuration = 10;
	[SerializeField] private bool buffed = false;
	[SerializeField] private bool debuffed = false;
	[SerializeField] private int debuffDuration = 10;

	[Header("Travel Variables")]
	[SerializeField] private float walkSpeed = 5;
	[SerializeField] private float runSpeed = 10;
	[SerializeField] private float speedMultiplier = 0.50f;
	[SerializeField] private Vector3 oldPosition;
	[SerializeField] private Vector3 thisPosition;
	[SerializeField] private Vector3 deltaPosition;

	private int buffTimer = 0;
	private int debuffTimer = 0;

	private float currentLogio = 25;

	private bool isDead = false;

	private CameraShakeOnCall cameraShake;

	void Start ()
	{
		// Find the player game object
		Player = GameObject.Find("Player");

		playerController = Player.GetComponent<FirstPersonController>();
		cameraShake = Player.GetComponent<CameraShakeOnCall>();
		oldPosition = gameObject.transform.position;

        currentHealth = startHealth;
        currentStamina = startStamina;
        currentImagi = startImagi;
        currentLogio = startLogio;
        currentVoid = startVoid;
            
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha9))
			ModifyHealth (-5);

		if (currentHealth <= 0)
		{
			IsDead = true;
		}

		if (!IsDead) 
		{
			HealthRegen ();
			StaminaRegen ();
			ImagiRegen();
			LogioRegen();
		}

		UpdatePositions();
		
		//
		//HandleParticles();

	}

	private void UpdatePositions()
	{
		thisPosition = gameObject.transform.position;
		deltaPosition = thisPosition - oldPosition;
		oldPosition = thisPosition;
	}
	#region Regenerations
	private void HealthRegen()
	{
		if (currentHealth == maxHealth)
			return;
		currentHealth = Mathf.Clamp ( currentHealth += healthRegen * Time.deltaTime, 0 ,maxHealth );
	}

	private void StaminaRegen()
	{
		Vector2 input = new Vector2 (CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
		if (input == Vector2.zero) 
		{
			currentStamina = Mathf.Clamp( currentStamina + staminaRegen * Time.deltaTime, 0, maxStamina );
		} 
		else // Regen at half the rate
		{
			if( playerController.IsWalking )
			{
				currentStamina = Mathf.Clamp( currentStamina + staminaRegen * 0.5f * Time.deltaTime, 0, maxStamina );
			}
			else // sprinting
			{
				currentStamina = Mathf.Clamp( currentStamina - staminaRegen * Time.deltaTime, 0, maxStamina );
			}
		}
	}

	private void ImagiRegen()
	{
		if (currentImagi >= maxImagi * 0.75f)
			return;
		currentImagi = Mathf.Clamp ( currentImagi += imagiRegen * Time.deltaTime, 0 , maxImagi * 0.75f );
	}

	private void LogioRegen()
	{
		if (currentLogio >= maxLogio * 0.25f)
			return;
		currentLogio = Mathf.Clamp ( currentLogio += logioRegen * Time.deltaTime, 0 , maxLogio * 0.25f );
	}

	public void ImagiHit()
	{
		buffTimer = buffDuration;
		if (!buffed) 
		{
			StartCoroutine("BuffTimer");
		}

	}

	public void DebuffPlayer()
	{
		debuffTimer = debuffDuration;
		if (!debuffed) 
		{
			StartCoroutine("DebuffTimer");
		}
	}

	public void VoidHit()
	{
		ModifyHealth (voidRegen);
	}

	IEnumerator BuffTimer ()
	{
		buffed = true;
		while (buffTimer > 0) 
		{
			buffTimer --;
			yield return new WaitForSeconds(1);
		}
		buffed = false;
		yield return null; //Done
	}

	IEnumerator DebuffTimer ()
	{
		debuffed = true;
		playerController.RunSpeed = runSpeed * speedMultiplier;
		playerController.WalkSpeed = walkSpeed * speedMultiplier;
		while (debuffTimer > 0) 
		{
			debuffTimer --;
			yield return new WaitForSeconds(1);
		}

		debuffed = false;
		playerController.RunSpeed = runSpeed;
		playerController.WalkSpeed = walkSpeed;
		yield return null;
	}

	#endregion

	public float CurrentHealth
	{
		get { return currentHealth; }
	}

	public float CurrentStamina
	{
		get { return currentStamina; }
	}

	// Add or subtract hp
	public void ModifyHealth(float amount)
	{
		currentHealth = Mathf.Clamp( currentHealth + amount, 0, maxHealth );
		cameraShake.ShakeViewport();
	}

	public void ModifyStamina(float amount)
	{
		currentStamina = Mathf.Clamp( currentStamina + amount, 0, maxStamina );
	}

	public void ModifyVoid(float amount)
	{
		currentVoid = Mathf.Clamp( currentVoid + amount, 0, maxVoid );
	}

	public void ModifyImagi(float amount)
	{
		currentImagi = Mathf.Clamp( currentImagi + amount, 0, maxImagi );
	}

	public void ModifyLogio(float amount)
	{
		currentLogio = Mathf.Clamp( currentLogio + amount, 0, maxLogio );
	}

	//drain mechanic

	public void ResetStats()
	{
		currentHealth = maxHealth;
		currentStamina = maxStamina;
	}

	public void ResetBuffs()
	{
		buffTimer = 0;
		debuffTimer = 0;
	}

	#region GeneralProperties
	public int StartHealth {
		get {
			return this.startHealth;
		}
		set {
			startHealth = value;
		}
	}

	public int MaxHealth {
		get {
			return this.maxHealth;
		}
		set {
			maxHealth = value;
		}
	}

	public int StartStamina {
		get {
			return this.startStamina;
		}
		set {
			startStamina = value;
		}
	}

	public int MaxStamina {
		get {
			return this.maxStamina;
		}
		set {
			maxStamina = value;
		}
	}

	public int StartVoid {
		get {
			return this.startVoid;
		}
		set {
			startVoid = value;
		}
	}

	public int MaxVoid {
		get {
			return this.maxVoid;
		}
		set {
			maxVoid = value;
		}
	}

	public float CurrentVoid {
		get {
			return this.currentVoid;
		}
		set {
			currentVoid = value;
		}
	}

	public int StartImagi {
		get {
			return this.startImagi;
		}
		set {
			startImagi = value;
		}
	}

	public int MaxImagi {
		get {
			return this.maxImagi;
		}
		set {
			maxImagi = value;
		}
	}

	public float CurrentImagi {
		get {
			return this.currentImagi;
		}
		set {
			currentImagi = value;
		}
	}

	public int StartLogio {
		get {
			return this.startLogio;
		}
		set {
			startLogio = value;
		}
	}

	public int MaxLogio {
		get {
			return this.maxLogio;
		}
		set {
			maxLogio = value;
		}
	}

	public float CurrentLogio {
		get {
			return this.currentLogio;
		}
		set {
			currentLogio = value;
	
		}
	}

	public bool IsDead
	{
		get{ return isDead; }
		set{ isDead = value; }
	}

	public bool Buffed
	{
		get 
		{
			return buffed;
		}
	
	}
	public bool Debuffed
	{
		get 
		{
			return debuffed;
		}
		
	}


	public float ImagiBuff
	{
		get 
		{
			return imagiBuff;
		}
		
	}

	public Vector3 DeltaPosition
	{
		get{ return deltaPosition;}
	}
	#endregion
}
