using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {
    GameObject mainCam;
    GameObject gvrController;
	// Use this for initialization
	void Start () {
        mainCam = this.gameObject.transform.Find("MainCamera").gameObject;
        gvrController = this.gameObject.transform.Find("GvrControllerPointer").gameObject;

        if (isLocalPlayer) {
            mainCam.SetActive(true);
            gvrController.SetActive(true);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
