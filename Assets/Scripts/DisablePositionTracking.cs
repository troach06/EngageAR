using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePositionTracking : MonoBehaviour {

    private void Awake()
    {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
