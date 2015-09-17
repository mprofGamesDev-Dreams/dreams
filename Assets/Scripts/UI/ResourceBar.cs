using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ResourceType{Health , Particles, Stamina, Power}
public enum ScrollDirection{Left, Right, Up, Down}

public class ResourceBar : MonoBehaviour {

	public RectTransform barTransform;
	private float cachedY;
	private float minX;
	private float maxX;
	private float currentTotal;
	[SerializeField] private int maxTotal;
	[SerializeField] private int startTotal;
	public Text BarText;
	public Image visualbar;
	public GameObject player;
	private PlayerStats playerRecord;

	public ResourceType BarType;
	public ScrollDirection ScrollType;
	// Use this for initialization
	void Start () {
		playerRecord = player.GetComponent<PlayerStats> ();
		barTransform = visualbar.rectTransform;
		cachedY = barTransform.position.y;
		maxX = barTransform.position.x;
		minX = barTransform.position.x - (barTransform.rect.width);
		startTotal = playerRecord.HealthStart;
		currentTotal = startTotal;
		maxTotal = playerRecord.HealthMax;

		switch(BarType)
		{
		case ResourceType.Health:
			startTotal = playerRecord.HealthStart;
			maxTotal = playerRecord.HealthMax;
			break;
		case ResourceType.Particles:
			startTotal = playerRecord.ParticlesStart;
			maxTotal = playerRecord.ParticlesMax;
			break;
		case ResourceType.Power:
			startTotal = playerRecord.PowerStart;
			maxTotal = playerRecord.PowerMax;
			break;
		case ResourceType.Stamina:
			startTotal = playerRecord.StaminaStart;
			maxTotal = playerRecord.StaminaMax;
			break;
		}

	}

	// Update is called once per frame
	void Update () {

		switch(BarType)
		{
		case ResourceType.Health:
			if (playerRecord.Health != currentTotal) 
			{
				currentTotal = playerRecord.Health;
				h_handleResource();
			}
			break;
		case ResourceType.Particles:
			if (playerRecord.Particles != currentTotal)
			{
				currentTotal = playerRecord.Particles;
				p_handleResource();
			}
			break;
		case ResourceType.Stamina:
			if(playerRecord.Stamina != currentTotal)
			{
				currentTotal = playerRecord.Stamina;
				s_handleResource();
			}
			break;
		case ResourceType.Power:
			if(playerRecord.Power != currentTotal)
			{
				currentTotal = playerRecord.Power;
				pw_handleResource();
			}
			break;
		}
	}
	private void h_handleResource()
	{
		BarText.text = ""+ currentTotal;
		float currentXVal = MapValues (currentTotal, 0, maxTotal, minX, maxX);

		barTransform.position = new Vector3 (currentXVal, cachedY);

		if (ScrollType == ScrollDirection.Right) 
		{
			barTransform.position = barTransform.position*-1;
		}

		if (currentTotal > maxTotal / 2) 
		{
			visualbar.color = new Color32((byte)MapHCValues(currentTotal,maxTotal/2,maxTotal,255,0),255,0,255);
		} 
		else 
		{
			visualbar.color = new Color32(255,(byte)MapHCValues(currentTotal,0,maxTotal/2,0,255),0,255);
		}
	}

	private void p_handleResource()
	{
		BarText.text = ""+ currentTotal;
		float currentXVal = MapValues (currentTotal, 0, maxTotal, minX, maxX);

		if (ScrollType == ScrollDirection.Right) 
		{
		//	barTransform.position = barTransform.position*-1;
		}

		barTransform.position = new Vector3 (currentXVal, cachedY);

	}

	private void s_handleResource()
	{
		BarText.text = ""+ currentTotal;
		float currentXVal = MapValues (currentTotal, 0, maxTotal, minX, maxX);

		if (ScrollType == ScrollDirection.Right) 
		{
		//	barTransform.position = barTransform.position*-1;
		}

		barTransform.position = new Vector3 (currentXVal, cachedY);

	}

	private void pw_handleResource()
	{
		BarText.text = ""+ currentTotal;
		float currentXVal = MapValues (currentTotal, 0, maxTotal, minX, maxX);

		if (ScrollType == ScrollDirection.Right) 
		{
			barTransform.position = barTransform.position*-1;
		}

		barTransform.position = new Vector3 (currentXVal, cachedY);

	}

		
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		switch (ScrollType) 
		{
		case ScrollDirection.Left:
			return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
			break;
		case ScrollDirection.Right:
			return (x - inMin) * (outMin-outMax) / (inMax - inMin) + outMax;
			break;
		}

		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private float MapHCValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
