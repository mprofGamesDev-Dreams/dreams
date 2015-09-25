using UnityEngine;
using System.Collections;

public class EnemyFast : MonoBehaviour 
{
	private NavMeshAgent navMeshAgent;
	private EnemyBehaviour behaviour;
	private EnemyScript stats;

	[SerializeField] private float speedMultiplier = 2;
	[SerializeField] private float damageMultiplier = 2;
	[SerializeField] [Range(0, 1)] private float healthMultiplier = 0.5f;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		behaviour = GetComponent<EnemyBehaviour>();
		stats = GetComponent<EnemyScript>();

		navMeshAgent.speed *= speedMultiplier;
		behaviour.attackDamage *= damageMultiplier;
		stats.MaxHealth *= healthMultiplier;
	}
}
