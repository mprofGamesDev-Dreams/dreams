using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float lineOfSightRange = 20.0f;
    private GameObject targetPlayer;
    private Vector3 original;
    private Vector3 target;
    private NavMeshAgent agent;
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

       

        if (Physics.Raycast(lineOfSight, out hit, lineOfSightRange))
        {

            if (hit.transform.gameObject.tag == "Player")
            {
                target = targetPlayer.transform.position;
                agent.SetDestination(target);
            }
            else
            {
                agent.SetDestination(original);
            }

           
        }
        else
        {
            agent.SetDestination(original);
        }

        
        
	}
}
