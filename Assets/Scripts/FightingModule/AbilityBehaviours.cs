/*
 * Script that implements magical abilities
 * Must be attached to the player character
 * Must be provided bullet prefabs that contain a "Bullet" script
 */

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class AbilityBehaviours : MonoBehaviour 
{
	private float logioTimer = 0;
	private float imagiTimer = 0;
	private float voidTimer = 0;

	// Serialize Private Fields To Show Up In The Insepctor
	[SerializeField] private float attackRange = 100.0f;
	[SerializeField] private GameObject logioBullet;
	[SerializeField] private GameObject imagiBullet;
	[SerializeField] private GameObject voidBullet;
	[SerializeField] private ParticleSystem myParticleSystem;

	[SerializeField] private PlayerStats playerStats;

	[Header("Skill Costs")]
	[SerializeField] [Range(0, 1)] private float logioPercent = 0;
	[SerializeField] [Range(0, 1)] private float voidPercent = 0;
	[SerializeField] [Range(0, 1)] private float imagiPercent = 0;

	[Header("Skill Cooldown")]
	[SerializeField] [Range(0, 5)] private float logioCD = 0;
	[SerializeField] [Range(0, 5)] private float imagiCD = 0;
	[SerializeField] [Range(0, 5)] private float voidCD = 0;

	private bool isLogioAvailable = true;
	private bool isImagiAvailable = true;
	private bool isVoidAvailable = true;
	private Transform myCameraTransform;
	private InputHandler input;
	private Color32 beamColor;
    [SerializeField]
    ActivePower currentPower; public ActivePower CurrentPower { get { return currentPower; } set { currentPower = value; } }
    [SerializeField] private bool logioLock = false;
    [SerializeField] private bool imagiLock = false;
    [SerializeField] private bool voidLock = false;

    AudioSource audioSourceBullets;
    [SerializeField] private AudioClip logioClip;
    [SerializeField] private AudioClip imagiClip;
    [SerializeField] private AudioClip voidClip;

    // Spellcasting Animation
    [Header("Spell Casting")]
    [SerializeField] private GameObject Arms;
    private Animator armAnimator;
    private bool isCasting;
    private bool cantCast;
    [SerializeField] private string TriggerName = "Spell";
    [SerializeField] private float animationCastFrame = 30;
    [SerializeField] private float framesPerSecond = 30;
    [SerializeField] private float animationPlaySpeed = 2;

	private void Start () 
	{
		// Gets The Main Camera's Transform On Object Startup
		myCameraTransform = Camera.main.GetComponent<Transform>();
		input = gameObject.GetComponent<InputHandler>();
		currentPower = ActivePower.Imagi;

		playerStats = GetComponent<PlayerStats>();

        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource temp in audioSources)
        {
            if (temp.name == "AudioSourceBullet")
            {
                audioSourceBullets = temp;
            }
        }

        armAnimator = Arms.GetComponent<Animator>();

        isCasting = false;
        cantCast = false;
	}
	
	private void Update () 
	{
		//Switches for next and previous powers
		if (input.isPrevPower ())
        {
			switch(currentPower)
            {
			    case ActivePower.Logio:
				    currentPower = ActivePower.Void;
				    break;
			    case ActivePower.Imagi:
				    currentPower = ActivePower.Logio;
				    break;
			    case ActivePower.Void:
				    currentPower = ActivePower.Imagi;
				    break;
			}
		}

		if (input.isNextPower ())
        {
			switch(currentPower)
            {
			    case ActivePower.Logio:
				    currentPower = ActivePower.Imagi;
				    break;
			    case ActivePower.Imagi:
				    currentPower = ActivePower.Void;
				    break;
			    case ActivePower.Void:
				    currentPower = ActivePower.Logio;
				    break;
			}
		}

        // Check if we can shoot
        // Providing we have pressed the button, arent paused, 
        //arent casting or waiting for cooldown and have the resources to do so
        if (input.isShoot() && Time.timeScale != 0 && !isCasting && !cantCast && canFire())
        {
            StartCoroutine(Cast());
        }
        else if(!input.isShoot() && !isCasting)
        {
            // Check for power switching
            if (CrossPlatformInputManager.GetButtonDown("Fire1") && !logioLock)//LOGIO
            {
                currentPower = ActivePower.Logio;
            }

            if (CrossPlatformInputManager.GetButtonDown("Fire2") && !imagiLock)//IMAGI
            {
                currentPower = ActivePower.Imagi;
            }

            if (CrossPlatformInputManager.GetButtonDown("Fire3") && !voidLock)//VOID
            {
                currentPower = ActivePower.Void;
            }
        }
	}

    public IEnumerator Cast()
    {

        isCasting = true;

        // Make sure we can actually cast before continuing
        while (cantCast)
        {
            yield return null;
        }

        // Play the arm animator
        armAnimator.SetTrigger(TriggerName);

        // Calculate how long to delay until we reach the 
        float spellDelay = ((1 / framesPerSecond) * animationCastFrame) / animationPlaySpeed;

        // Wait for the clip (castAnimation.clip.length)
        yield return new WaitForSeconds(spellDelay);

        // Create the spell bullet
        shootBullet();

        // Flag we are not casting
        isCasting = false;

        // Flag that we are on cooldown
        cantCast = true;

        // Look at our current power and reduce resources
        // Also invoke cooldown
        switch (currentPower)
        {
            case ActivePower.Imagi:
                playerStats.CurrentImagi -= (playerStats.MaxImagi * imagiPercent);
                isImagiAvailable = false;
                while (imagiTimer < imagiCD)
                {
                    imagiTimer += Time.deltaTime;
                    yield return null;
                }
                imagiTimer = 0;
                isImagiAvailable = true;
                break;
            case ActivePower.Logio:
                playerStats.CurrentLogio -= (playerStats.MaxLogio * logioPercent);
                isLogioAvailable = false;
                while (logioTimer < logioCD)
                {
                    logioTimer += Time.deltaTime;
                    yield return null;
                }
                logioTimer = 0;
                isLogioAvailable = true;
                break;
            case ActivePower.Void:
                playerStats.CurrentVoid -= (playerStats.MaxVoid * voidPercent);
                isVoidAvailable = false;
                while (voidTimer < voidCD)
                {
                    voidTimer += Time.deltaTime;
                    yield return null;
                }
                voidTimer = 0;
                isVoidAvailable = true;
                break;
        }

        // Allow for casting again
        cantCast = false;
    }

	private bool canFire()
	{
		if (currentPower == ActivePower.Imagi && playerStats.CurrentImagi >= ( playerStats.MaxImagi * imagiPercent ) && isImagiAvailable ) 
		{
			return true;
		}

		if (currentPower == ActivePower.Logio && playerStats.CurrentLogio >= ( playerStats.MaxLogio * logioPercent ) && isLogioAvailable ) 
		{
			return true;
		}

		if (currentPower == ActivePower.Void && playerStats.CurrentVoid >= ( playerStats.MaxVoid * voidPercent ) && isVoidAvailable ) 
		{
			return true;
		}

		return false;
	}

	public void setActivePower(ActivePower ap)
	{
		currentPower = ap;
	}

	private void shootRay()
	{

		switch(currentPower)
		{
		case ActivePower.Logio:
			beamColor = Color.yellow;
			break;
		case ActivePower.Imagi:
			beamColor = Color.cyan;
			break;
		case ActivePower.Void:
			beamColor = Color.blue;
			break;
		}
		
		RaycastHit hit;
		Ray pointer = new Ray(myCameraTransform.position, myCameraTransform.forward * attackRange);
		
		Debug.DrawRay(pointer.origin, pointer.direction, beamColor);
		
		myParticleSystem.startColor = beamColor;
		myParticleSystem.Emit(1);
		
		if (Physics.Raycast(pointer, out hit, attackRange))
		{
			Shield shieldScript = hit.transform.GetComponent<Shield>();
			if(shieldScript != null)
			{
				switch(currentPower)
				{
				case ActivePower.Logio:
					shieldScript.ApplyDamage(Shield.shieldOptions.Logio, 10	);
					break;
				case ActivePower.Imagi:
					shieldScript.ApplyDamage(Shield.shieldOptions.Imagi, 10	);
					break;
				case ActivePower.Void:
					shieldScript.ApplyDamage(Shield.shieldOptions.Void, 10	);
					break;
					
				}
			}
		}
	}

	private void shootBullet()
	{
		GameObject bullet;
		switch (currentPower) 
		{
		case ActivePower.Logio:
			bullet = (GameObject)Instantiate(logioBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation);
			bullet.GetComponent<Bullet>().SetParent(gameObject);
            audioSourceBullets.PlayOneShot(logioClip);
			break;
		case ActivePower.Imagi:
			bullet = (GameObject)Instantiate(imagiBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation); 
			bullet.GetComponent<Bullet>().SetParent(gameObject);
            audioSourceBullets.PlayOneShot(imagiClip);
			break;
		case ActivePower.Void:
			bullet = (GameObject)Instantiate(voidBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation); 
			bullet.GetComponent<Bullet>().SetParent(gameObject);
            audioSourceBullets.PlayOneShot(voidClip);
			break;
		}

        
	}

	public ActivePower getCurrentPower()
    {
		return currentPower;
	}

	public float LogioTimer
	{
		get{return logioTimer;}
	}
	public float ImagiTimer
	{
		get{return imagiTimer;}
	}
	public float VoidTimer
	{
		get{return voidTimer;}
	}
	public float ImagiCD
	{
		get{return imagiCD;}
	}
	public float LogioCD
	{
		get{return logioCD;}
	}
	public float VoidCD
	{
		get{return voidCD;}
	}
}



