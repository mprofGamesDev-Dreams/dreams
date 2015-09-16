using UnityEngine;
using System.Collections;

public class AbilityBehaviours : MonoBehaviour {

    float attackRange = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

            //RaycastHit hit;
            Ray debugPointer = new Ray(GetComponentInParent<Camera>().transform.position, GetComponentInParent<Camera>().transform.forward * attackRange);

            Debug.DrawRay(debugPointer.origin, debugPointer.direction);


            if (Input.GetKeyDown(KeyCode.L))//LOGIO
            {
                RaycastHit hit;
                Ray pointer = new Ray(GetComponentInParent<Camera>().transform.position, GetComponentInParent<Camera>().transform.forward * attackRange);

                Debug.DrawRay(pointer.origin, pointer.direction);

                GetComponent<ParticleSystem>().startColor = Color.yellow;
                GetComponent<ParticleSystem>().Emit(1);

                if (Physics.Raycast(pointer, out hit, attackRange))
                {
                    hit.transform.gameObject.SendMessage("applyLogioDamage", 10);
                }
            }

            if (Input.GetKeyDown(KeyCode.M))//IMAGI
            {
                RaycastHit hit;
                Ray pointer = new Ray(GetComponentInParent<Camera>().transform.position, GetComponentInParent<Camera>().transform.forward * attackRange);

                Debug.DrawRay(pointer.origin, pointer.direction);

                GetComponent<ParticleSystem>().startColor = Color.cyan;
                GetComponent<ParticleSystem>().Emit(1);

                if (Physics.Raycast(pointer, out hit, attackRange))
                {
                    hit.transform.gameObject.SendMessage("applyImagiDamage", 10);
                }
            }

            if (Input.GetKeyDown(KeyCode.V))//VOID
            {
                RaycastHit hit;
                Ray pointer = new Ray(GetComponentInParent<Camera>().transform.position, GetComponentInParent<Camera>().transform.forward * attackRange);

                Debug.DrawRay(pointer.origin, pointer.direction);

                GetComponent<ParticleSystem>().startColor = Color.blue;
                GetComponent<ParticleSystem>().Emit(1);

                if (Physics.Raycast(pointer, out hit, attackRange))
                {
                    hit.transform.gameObject.SendMessage("applyVoidDamage", 10);
                }
            }
        }
}
