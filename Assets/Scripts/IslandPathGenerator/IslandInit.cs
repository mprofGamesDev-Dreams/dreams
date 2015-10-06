using UnityEngine;
using System.Collections;

public class IslandInit : MonoBehaviour {

	public Transform islandStart;
	public EnemyScript enemyTrigger;
	public GameObject EnemyContainer;

	public bool HasEnemy;

	// Use this for initialization
	void Start ()
	{
		// Translate children by start offset
		Transform EndPos = this.gameObject.transform.GetChild(1);
		EndPos.position -= islandStart.localPosition;
		
		// Set position to start position
		transform.position = islandStart.position;
		
		// Translate children by start offset
		Transform StartPos = this.gameObject.transform.GetChild(0);
		Destroy (StartPos.gameObject);

		// Flag we had an enemy at creation
		if (enemyTrigger)
			HasEnemy = true;
	}
	
	// Update is called once per frame
	void Update ()
	{ /*
		Commented out by GC
		If you are finding that your build is no longer moving islands in do not be alarmed
		The final encounter neeeds to be set up. Please refer to the FinalFightScript for more details.
		
		// If we have an enemy trigger
		if (enemyTrigger)
		{
			// If its health is low enough
			if (enemyTrigger.Health < enemyTrigger.MaxHealth * 0.5)
			{
				// Activate trigger
				gameObject.GetComponent<TransformGeometry> ().Trigger ();
			}
		}
		else if(enemyTrigger == null) // Dont have an enemy trigger
		{
			// If we did have an enemy trigger then activate
			if(HasEnemy)
			{
				gameObject.GetComponent<TransformGeometry> ().Trigger ();
			}
			else if( EnemyContainer ) // We have a platform to search
			{
				if( EnemyContainer.GetComponent<TransformGeometry>().IsTriggered() )
				{
					// Look to see if the platform has a child
					if( !EnemyContainer.transform.FindChild("Enemy"))
					{
						gameObject.GetComponent<TransformGeometry> ().Trigger ();
					}
				}
			}
		}*/
	}
}
