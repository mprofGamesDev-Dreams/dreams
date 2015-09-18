using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float lineOfSightRange = 20.0f;
    private GameObject targetPlayer;
    private Vector3 original;
    private Vector3 target;
    public GameObject[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPatrolNode = 0;
    private enum aiStates { follow, patrol, returnToPosition, attack};
    private aiStates aiState = aiStates.follow;
	// Use this for initialization

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        target = transform.position;
        original = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Ray lineOfSight = new Ray(gameObject.transform.position, targetPlayer.transform.position - gameObject.transform.position);

        Debug.DrawRay(lineOfSight.origin,lineOfSight.direction*lineOfSightRange);




        switch (aiState)
        {
            case aiStates.follow:
                target = targetPlayer.transform.position;
                agent.SetDestination(target);
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
                break;

           case aiStates.returnToPosition:
                agent.SetDestination(original);
                break;

            case aiStates.attack:
                break;

            default:
                Debug.LogError("AI state not set correctly");
                break;
        }


        if (Physics.Raycast(lineOfSight, out hit, lineOfSightRange/*,LayerMask.NameToLayer("Level")*/))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                aiState = aiStates.follow;
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

        
        
	}
}
