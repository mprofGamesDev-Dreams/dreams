using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResourceBar : MonoBehaviour {

	public RectTransform barTransform;
	private float cachedY;
	private float minX;
	private float maxX;
	private int currentTotal;
	[SerializeField] private int maxTotal;
	[SerializeField] private int startTotal;
	public Text BarText;
	public Image visualbar;
	public GameObject player;
	private TestRecord playerRecord;
	// Use this for initialization
	void Start () {
		playerRecord = player.GetComponent<TestRecord> ();
		barTransform = visualbar.rectTransform;
		cachedY = barTransform.position.y;
		maxX = barTransform.position.x;
		minX = barTransform.position.x - (barTransform.rect.width);
		startTotal = playerRecord.startHealth;
		currentTotal = startTotal;
		maxTotal = playerRecord.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerRecord.health != currentTotal) 
		{
			currentTotal = playerRecord.health;
			handleResource();
		}
	}
	private void handleResource()
	{
		BarText.text = ""+ currentTotal;
		float currentXVal = MapValues (currentTotal, 0, maxTotal, minX, maxX);

		barTransform.position = new Vector3 (currentXVal, cachedY);

		if (currentTotal > maxTotal / 2) 
		{
			visualbar.color = new Color32((byte)MapValues(currentTotal,maxTotal/2,maxTotal,255,0),255,0,255);
		} 
		else 
		{
			visualbar.color = new Color32(255,(byte)MapValues(currentTotal,0,maxTotal/2,0,255),0,255);
		}
	}

		
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
