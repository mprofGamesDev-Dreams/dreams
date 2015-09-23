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


	private int attTrig;
	private bool canAttack = true;
	private Transform cameraTransform;
	private InputHandler input;

	private void Start () 
	{
		attTrig = Animator.StringToHash(attackTrigger);

		cameraTransform = Camera.main.GetComponent<Transform>();
		input = gameObject.GetComponent<InputHandler> ();

		if( attackToNormalizedTime < attackFromNormalizedTime ) 
		{
			Debug.LogError("attackToNormalizedTime < attackFromNormalizedTime");
		}
	}
	
	private void Update () 
	{
		if (input.isMelee ()) AttackEvent ();

		if(animationController.GetCurrentAnimatorStateInfo(0).IsName("Punch") && canAttack)
		{
			if(animationController.GetCurrentAnimatorStateInfo(0).normalizedTime > attackFromNormalizedTime && animationController.GetCurrentAnimatorStateInfo(0).normalizedTime < attackToNormalizedTime )
				Attack();
		}

		if(!animationController.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
		{
			canAttack = true;
		}
	}

	/// <summary>
	/// Event called from the InputHandler script
	/// </summary>
	public void AttackEvent()
	{
		if(!animationController.GetCurrentAnimatorStateInfo(0).IsName("AttackTrigger"))
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
				es.TakeSA();
			}
		}
#if UNITY_EDITOR
		else Debug.DrawRay(cameraTransform.position, cameraTransform.forward * attackRange, Color.red);
#endif
	}
}
