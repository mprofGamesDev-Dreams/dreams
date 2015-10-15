using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float lineOfSightRange = 30.0f;
    public float attackDamage = 10.0f;
    public float attackRange = 3.0f;
    public float attackCooldown = 0.5f;
   
    private float attackTimer = 0.0f;
    private GameObject targetPlayer;
    private Vector3 original;
    private Vector3 target;
    public GameObject[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPatrolNode = 0;
    private enum aiStates { follow, patrol, returnToPosition, attack};
    private aiStates aiState = aiStates.follow;
    private bool playerSeen = false;

    public LayerMask masksToUse;

	public bool isBig;

	[SerializeField] private bool canSlow = false;

	private Vector3 OldPosition;

	
	// Animator
	[SerializeField] private GameObject rigPrefab;
	private Animator rigAnimation;
	
	// Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
        original = transform.position;
		OldPosition = transform.position;

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

		// Make sure our new move position is safe
		if (!Physics.Raycast(transform.position, -Vector3.up, 1.0f))
		{
			// Position is not safe so go back to old position
			transform.position = OldPosition;
		}


		if (targetPlayer == null)
			targetPlayer = GameObject.FindGameObjectWithTag ("Player");

        Vector3 rayCastOrigin = new Vector3(0.0f, 2.0f, 0.0f);


        RaycastHit hit;
        Ray lineOfSight = new Ray(gameObject.transform.position + rayCastOrigin, targetPlayer.transform.position - (gameObject.transform.position + rayCastOrigin));

        Debug.DrawRay(lineOfSight.origin,lineOfSight.direction*lineOfSightRange);

        if (Physics.Raycast(lineOfSight, out hit, lineOfSightRange, masksToUse))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                aiState = aiStates.follow;

                float tempDistance = Vector3.Distance(hit.transform.position, gameObject.transform.position);

                if (tempDistance < attackRange)
                {
                    aiState = aiStates.attack;
                }
            }
            else
            {
                if (patrolPoints.GetLength(0) == 0)
                {
                    aiState = aiStates.returnToPosition;
                }
                else
                {
                    aiState = aiStates.patrol;
                }
            }
        }
        else
        {
            if (patrolPoints.GetLength(0) == 0)
            {
                aiState = aiStates.returnToPosition;
            }
            else
            {
                aiState = aiStates.patrol;
            }
        }

        switch (aiState)
        {
            case aiStates.follow:
                target = targetPlayer.transform.position;
				if(agent.isOnNavMesh){
					agent.SetDestination(target);
				}
					
				playerSeen = true;

				rigAnimation.SetBool("Attack", false);
                
                break;

           case aiStates.patrol:
                target = patrolPoints[currentPatrolNode].transform.position;

				if(agent.isOnNavMesh)
					agent.SetDestination(target);
                
                
                Vector3 tempVector = transform.position - patrolPoints[currentPatrolNode].transform.position;
                
                if (tempVector.magnitude < 1.0f)
                {
                    currentPatrolNode++;
                    if (currentPatrolNode == patrolPoints.Length)
                    {
                        currentPatrolNode = 0;
                    }
				}

				rigAnimation.SetBool("Attack", false);

                playerSeen = false;
                break;

           case aiStates.returnToPosition:
				if(agent.isOnNavMesh)
                	agent.SetDestination(original);

                playerSeen = false;
			
				rigAnimation.SetBool("Attack", false);
                break;

            case aiStates.attack:
				
				if(!isBig)
				{
					if(agent.isOnNavMesh)
                		agent.SetDestination(transform.position);
				}

                if (attackTimer >= attackCooldown)
                {
					rigAnimation.SetBool("Attack", true);
					PlayerStats hitStats =  hit.transform.gameObject.GetComponent<PlayerStats>();
					hitStats.SendMessage("ModifyHealth", -attackDamage);
					if(canSlow)
					hitStats.DebuffPlayer();
					
                    attackTimer = 0.0f;
                }
                else
				{
                    attackTimer += Time.deltaTime;
                }
                playerSeen = false;
                break;

            default:
                Debug.LogError("AI state not set correctly");
                break;
        } 

		// Store current position
		OldPosition = agent.nextPosition;
	}

    public bool GetPlayerSeen()
    {
        return playerSeen;
    }
}
