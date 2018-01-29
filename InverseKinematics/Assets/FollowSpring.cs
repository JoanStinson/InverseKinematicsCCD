using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpring : MonoBehaviour {
    public Transform target;
    public float speedPosition = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position;
    }
}
