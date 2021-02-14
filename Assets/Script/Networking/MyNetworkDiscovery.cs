using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Discovery;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MyNetworkDiscovery : NetworkDiscovery
{
    MainMenu mainMenuObject;

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            mainMenuObject = GameObject.FindObjectOfType<MainMenu>();
            OnServerFound.RemoveAllListeners();
            OnServerFound.AddListener(mainMenuObject.DisplayRoomList);
        }
    }
}
