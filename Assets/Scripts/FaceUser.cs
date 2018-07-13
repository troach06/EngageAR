using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion r1 = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 euler2 = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
    }
}
