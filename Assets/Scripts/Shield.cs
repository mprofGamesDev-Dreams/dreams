using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
    public enum shieldOptions { Imagi, Logio, Void };
    
	[SerializeField] private shieldOptions shieldType;
    [SerializeField] private int shieldHealth = 20;

    private float scrollOffset = 0;
    private float scrollSpeed = 0.003f;

    private void Start()
    {
    }

	private void Update () 
	{
       if (shieldHealth <= 0)
        {
			Destroy(gameObject);
        }

       // scrollOffset += scrollSpeed;
        //GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(scrollOffset / 2, scrollOffset));
	}

    public void ApplyDamage( shieldOptions damageType, int damageIn)
    {
        if (shieldType == damageType)
        {
            shieldHealth -= damageIn;
        }
    }
}
