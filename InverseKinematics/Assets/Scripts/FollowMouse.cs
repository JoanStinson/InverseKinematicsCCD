using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public GameObject target, father, child;
    private float height = 5f, increase = 0.1f, originalHeight = 5f;

    void Update() {

        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKey(KeyCode.E)) {
            height += increase;
            if (height > 90f) height = increase;
        } else if (Input.GetKey(KeyCode.Q)) {
            height -= increase;
            if (height < 0f) height = 90f;
        } else if (Input.GetKey(KeyCode.R)) {
            height = originalHeight;
        } else if (Input.GetKey(KeyCode.A)) {
            child.transform.position = father.transform.position;
            child.transform.position += new Vector3(0, 1.1f, 0);
            child.transform.parent = father.transform;
        } else if (Input.GetKey(KeyCode.D)) {
            father.transform.DetachChildren();
        }

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity)) {
            target.transform.position = hit.point;
            target.transform.position += new Vector3(0, height, 0);
        }
    }
}
