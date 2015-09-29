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
    
	// Use this for initialization

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
        original = transform.position;

    }

	// Update is called once per frame
	void Update () {

		if (targetPlayer == null)
			targetPlayer = GameObject.FindGameObjectWithTag ("Player");

        RaycastHit hit;
        Ray lineOfSight = new Ray(gameObject.transform.position, targetPlayer.transform.position - gameObject.transform.position);

        Debug.DrawRay(lineOfSight.origin,lineOfSight.direction*lineOfSightRange);

        

        if (Physics.Raycast(lineOfSight, out hit, lineOfSightRange/*,LayerMask.NameToLayer("Level")*/))
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
                agent.SetDestination(target);
                playerSeen = true;
                
                break;

           case aiStates.patrol:
                target = patrolPoints[currentPatrolNode].transform.position;
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

                playerSeen = false;
                break;

           case aiStates.returnToPosition:
                agent.SetDestination(original);
                playerSeen = false;
                break;

            case aiStates.attack:
                agent.SetDestination(transform.position);
                if (attackTimer >= attackCooldown)
                {
					
					PlayerStats hitStats =  hit.transform.gameObject.GetComponent<PlayerStats>();
					hitStats.SendMessage("ModifyHealth", -attackDamage);
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
	}

    public bool GetPlayerSeen()
    {
        return playerSeen;
    }
}
