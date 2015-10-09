using UnityEngine;
using System.Collections;

public class PunchingControllerPrototype : MonoBehaviour 
{
	[SerializeField] private string attackTrigger;
	[SerializeField] private float attackRange;

	[SerializeField][Range(0,1)] private float attackFromNormalizedTime;
	[SerializeField][Range(0,1)] private float attackToNormalizedTime;

	[SerializeField] private Animator animationController;

	[SerializeField]private LayerMask raycastTargets;

	[SerializeField] private GameObject parent;

	private int attTrig;
	private bool canAttack = true; // Controls the raycast
	private Transform cameraTransform;
	private InputHandler input;

	private int lastInput = 0; public int LastInput { get { return lastInput; } set { lastInput = value; } }

    private AudioSource audioSourcePunch;
    [SerializeField] private AudioClip punchSwing;
    [SerializeField] private AudioClip punchImpact;

	private void Start () 
	{
		attTrig = Animator.StringToHash(attackTrigger);

		cameraTransform = Camera.main.GetComponent<Transform>();
        //input = gameObject.GetComponent<InputHandler> ();

		input = parent.GetComponent<InputHandler>();

		if( attackToNormalizedTime < attackFromNormalizedTime ) 
		{
			Debug.LogError("attackToNormalizedTime < attackFromNormalizedTime");
		}

        AudioSource[] audioSources = parent.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource temp in audioSources)
        {
            if (temp.name == "AudioSourcePunch")
            {
                audioSourcePunch = temp;
            }
        }
	}
	
	private void Update () 
	{
		if (input.isMelee () && lastInput == 0) 
		{
			lastInput = 1;
			AttackEvent ();
            Invoke("PunchAudio",0.4f);
		}

		if(animationController.GetCurrentAnimatorStateInfo(0).IsName("Punch") && canAttack)
		{
            if (animationController.GetCurrentAnimatorStateInfo(0).normalizedTime > attackFromNormalizedTime && animationController.GetCurrentAnimatorStateInfo(0).normalizedTime < attackToNormalizedTime)
            {
                Attack();
            }
		}

		if(!animationController.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
		{
			canAttack = true;
		}
	}

    private void PunchAudio()
    {
        audioSourcePunch.PlayOneShot(punchSwing);
    }

	/// <summary>
	/// Event called from the InputHandler script
	/// </summary>
	public void AttackEvent()
	{
		if(!animationController.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
		{
			animationController.SetTrigger( attTrig );
            
		}
	}

	private void Attack()
	{
		RaycastHit hit;
		if(Physics.Raycast( cameraTransform.position, cameraTransform.forward, out hit, attackRange, raycastTargets ))
		{
#if UNITY_EDITOR
			Debug.DrawLine(cameraTransform.position, hit.point, Color.green);
#endif	 
			EnemyScript es;
			if((es = hit.collider.GetComponent<EnemyScript>()) != null)// Is Enemy
			{
				canAttack = false;
				if (parent.GetComponent<PlayerStats>().Buffed)
				{
					es.TakeDamage( 2 * parent.GetComponent<PlayerStats>().ImagiBuff);
                    audioSourcePunch.PlayOneShot(punchImpact);
				}
				else
				{
					es.TakeDamage(2);
                    audioSourcePunch.PlayOneShot(punchImpact);
				}


			}
		}
#if UNITY_EDITOR
		else Debug.DrawRay(cameraTransform.position, cameraTransform.forward * attackRange, Color.red);
#endif
	}
}
