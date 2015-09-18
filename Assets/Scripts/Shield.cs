using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
    public enum shieldOptions { Imagi, Logio, Void };
    
	[SerializeField] private shieldOptions shieldType;
    [SerializeField] private int shieldHealth = 20;

	private void Update () 
	{
        if (shieldHealth <= 0)
        {
			Destroy(gameObject);
        }
	}

    public void ApplyDamage( shieldOptions damageType, int damageIn)
    {
        if (shieldType == damageType)
        {
            shieldHealth -= damageIn;
        }
    }
}
