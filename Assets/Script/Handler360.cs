using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Handler360 : NetworkBehaviour
{
    [SyncVar]
    public int skyBoxIndex;

    public static int sceneNumber = 0;
    public Material[] skyboxes;

    public GameObject informationPanel;


    public bool isSliding = false;
    public int slidingState = 0;

    public Slider[] slidersGuide;

    // Use this for initialization
    void Start()
    {
        XRSettings.enabled = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = skyboxes[skyBoxIndex];
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        if (isSliding)
        {
            slidersGuide[slidingState].value += 0.3f * Time.deltaTime;
        }


        if (slidersGuide[slidingState].value >= 1)
        {
            switch (slidingState)
            {
                case 0: //tour 1
                    SwitchSkybox(0);
                    break;
                case 1: //tour 2
                    SwitchSkybox(1);
                    break;
                case 2: //tour 3
                    SwitchSkybox(2);
                    break;

            }

        }

    }



    public void SlidingStage(int stage)
    {
        slidingState = stage;
        isSliding = true;
    }

    public void CancelSlididng()
    {
        slidersGuide[slidingState].value = 0;
        isSliding = false;
        foreach (Slider wholeSlider in slidersGuide)
        {
            wholeSlider.value = 0;
        }
    }

   

    public void OpenInfo(bool isShow)
    {
        if (isShow)
            informationPanel.SetActive(true);
        else
            informationPanel.SetActive(false);

    }

    
    public void SwitchSkybox(int index) {
        skyBoxIndex = index;   
    }

}
