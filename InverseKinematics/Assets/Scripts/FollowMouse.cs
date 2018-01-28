using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public GameObject target;
    private float height = 40f, increase = 0.5f, originalHeight = 50f;

    void Update() {

        if (Input.GetKey(KeyCode.W)) {
            target.transform.position += new Vector3(0, increase, 0);
        }
        else if (Input.GetKey(KeyCode.S)) {
            target.transform.position -= new Vector3(0, increase, 0);
        }
        else if (Input.GetKey(KeyCode.R)) {
            height = originalHeight;
        }
        else if (Input.GetKey(KeyCode.Q)) {
            /*target.transform.position = father.transform.position;
            target.transform.position += new Vector3(0, 4f, 0);
            target.transform.parent = father.transform;*/
            target.GetComponent<ParticleController>().enabled = false;
        }
        else if (Input.GetKey(KeyCode.E)) {
            //father.transform.DetachChildren();
            target.GetComponent<ParticleController>().enabled = true;
            //child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        
    }
}
