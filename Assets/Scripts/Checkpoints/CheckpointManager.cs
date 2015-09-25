using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {
	
	[SerializeField] private GameObject player;
    [SerializeField]
    private const float death_level = -40;
	private PlayerStats stats;
	private Vector3 last_pos;
	
	// Use this for initialization
	void Start () {
		stats = player.GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y < death_level)
			stats.IsDead = true;
		
		if (stats.IsDead) {
			player.transform.position = last_pos;
			stats.ResetBuffs();
			stats.ResetStats();
			stats.IsDead = false;
		}
	}
	
	public void SetLastPos(Vector3 last_pos){
		this.last_pos = last_pos;
	}
}
