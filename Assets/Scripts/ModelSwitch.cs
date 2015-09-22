using UnityEngine;
using System.Collections;

public class ModelSwitch : MonoBehaviour {

    public GameObject modelToSwap;
    private GameObject newObject;
    private bool triggered = false;
    private bool switching = false;
    public float switchDuration = 1.0f;
    [SerializeField] private float currentTime = 0.0f;
    public AnimationCurve switchCurve;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))//TODO//BSD//21-09-15//Change to correct event trigger
        {
            triggered = true;
        }


        if (triggered&&!switching)
        {
            SwitchModel();
            triggered = false;
            switching = true;
        }

        if (switching)
        {
            currentTime += Time.deltaTime;

            float curveXPosition = currentTime * (1/switchDuration);
            float curveYPosition = switchCurve.Evaluate(curveXPosition);
            newObject.transform.localScale = new Vector3(curveYPosition, curveYPosition, curveYPosition);
            gameObject.transform.localScale = new Vector3(1-curveYPosition, 1-curveYPosition, 1-curveYPosition);

            //newObject.transform.localScale = new Vector3(newObject.transform.localScale.x + switchRate, newObject.transform.localScale.z + switchRate, newObject.transform.localScale.z + switchRate);
            //gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - switchRate, gameObject.transform.localScale.z - switchRate, gameObject.transform.localScale.z - switchRate);


            if (currentTime >= switchDuration)
            {
                switching = false;
                //Destroy(gameObject);
            }
        }


	}

    void SwitchModel()
    {
        newObject = (GameObject)Instantiate(modelToSwap, transform.position, transform.rotation);
        newObject.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
    }
}
