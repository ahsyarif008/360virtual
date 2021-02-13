using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetupLocalPlayer : NetworkBehaviour {
    public GameObject mainCam;
    public GameObject gvrController;
	// Use this for initialization
	void Start () {
        if (isLocalPlayer) {
            mainCam.SetActive(true);
          //  gvrController.SetActive(true);
        } else {
            mainCam.SetActive(false);
           // gvrController.SetActive(false);
        }
		
	}

}
