using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public enum shieldOptions { Imagi, Logio, Void };
    public shieldOptions shieldType;

    private int shieldHealth = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (shieldHealth <= 0)
        {
            //GetComponent<MeshRenderer>().enabled = false;
			Destroy(gameObject);
        }
	}

    void applyImagiDamage(int damageIn)
    {
        if (shieldType == shieldOptions.Imagi)
        {
            shieldHealth -= damageIn;
        }
    }

    void applyLogioDamage(int damageIn)
    {
        if (shieldType == shieldOptions.Logio)
        {
            shieldHealth -= damageIn;
        }
    }

    void applyVoidDamage(int damageIn)
    {
        if (shieldType == shieldOptions.Void)
        {
            shieldHealth -= damageIn;
        }
    }
}
