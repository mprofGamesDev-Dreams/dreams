using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {
	
	[SerializeField] private GameObject player;
    [SerializeField]
    private const float deathLevel = -40;
	private PlayerStats stats;
	private Vector3 lastPos;
	[SerializeField] private BlackScreen blackScreen;
	private InputHandler playerInputManager;
	
	// Use this for initialization
	void Start () {
		stats = player.GetComponent<PlayerStats> ();
		lastPos = GetComponent<Transform>().position;

		playerInputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y < deathLevel)
			stats.IsDead = true;
		
		if (stats.IsDead) {
			blackScreen.RequestFlash();
			// Disable player movement while they are being respawned
			playerInputManager.ControllerConstraints = EControlConstraints.DisableAll;
			StartCoroutine(RespawnPlayer());
		}
	}
	
	public void SetLastPos(Vector3 last_pos){
		this.lastPos = last_pos;
	}

	IEnumerator RespawnPlayer(){
		//Delay respawning to allow screen to fade completely to black
		yield return new WaitForSeconds (1.0f);
		// Respawn player at last checkpoint reached, and reset their stats
		player.transform.position = lastPos;
		stats.ResetBuffs();
		stats.ResetStats();
		stats.IsDead = false;
		// Re-enable player control
		playerInputManager.ControllerConstraints = EControlConstraints.EnableAll;
	}
}
