using UnityEngine;
using System.Collections;

/*ModelSwitch
 * 
 * The game object this script is attached to, scales according to the animation curve from end to start when triggered
 * This script generates another game object when triggered which scales according to the animation curve
 * This script requires the Trigger() function to be called
 */

public class ModelSwitch : MonoBehaviour {

    public GameObject modelToSwap;
    private GameObject newObject;

    private bool triggered = false;
    private bool switching = false;

    public float switchDuration = 1.0f;
    public AnimationCurve switchCurve;

    private float currentTime = 0.0f;
    

	void Update () 
    {
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

            if (currentTime >= switchDuration)
            {
                switching = false;
                Destroy(gameObject);
            }
        }


	}

    void Trigger()
    {
        triggered = true;
    }

    void SwitchModel()
    {
        newObject = (GameObject)Instantiate(modelToSwap, transform.position, transform.rotation);
        newObject.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
    }
}
