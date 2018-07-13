using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AndroidPermissionsManager.RequestPermission("android.permission.RECORD_AUDIO");
        AndroidPermissionsManager.RequestPermission("android.permission.INTERNET");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
