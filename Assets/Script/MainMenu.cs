using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;
using Mirror.Discovery;
using Newtonsoft.Json;
using Mirror;


public class MainMenu : MonoBehaviour
{
    [Header("Menu Element")]
    public Transform parentMenu;
    public GameObject prefabMenuItem;

    [Header("Other")]
    public Text btnClientLabel, txtClientTitle;
    public GameObject learningSelectScreen, passwordScreen;

    enum AppState { main, pengembang, petunjuk, selectMode, selectMateri, selectRoom };
    AppState appState;

    //game component
    GameObject[] screens;
    public GameObject modeUI, materiUI, pengembangUI, petunjukUI, roomUI;
    public GameObject loadingScreen, btnBack, btnClientStart;

    public MyNetworkManager networkManager;
    public MyNetworkDiscovery networkDiscovery;

    // Use this for initialization
    void Start()
    {
        XRSettings.enabled = false;
        txtClientTitle.text = "Tunggu pemandu memulai server ...........";

        networkManager = Singleton.Instance.networkManager;
        networkDiscovery = Singleton.Instance.networkDiscovery;


        screens = new GameObject[] { modeUI, materiUI, pengembangUI, petunjukUI, roomUI };
        NavigateTo("main");
        SetupLessonMenuList();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }

    void SetupLessonMenuList()
    {
        MaterialSubtheme[] materialSubthemes = Singleton.Instance.materialSubthemes;
        for (int i = 0; i < materialSubthemes.Length; i++)
        {
            int x = i;
            //setup subtheme
            GameObject item = Instantiate(prefabMenuItem, Vector2.zero, Quaternion.identity, parentMenu);
            item.GetComponent<RectTransform>().localScale = Vector2.one;

            MapItemMenu mapItemMenu = item.GetComponent<MapItemMenu>();
            mapItemMenu.txtMenuItem.text = materialSubthemes[i].subthemeName;
            mapItemMenu.btnAction.interactable = false;

            for (int j = 0; j < materialSubthemes[i].lessons.Length; j++)
            {
                int y = j;
                //setup lesson
                GameObject itemLesson = Instantiate(prefabMenuItem, Vector2.zero, Quaternion.identity, parentMenu);
                itemLesson.GetComponent<RectTransform>().localScale = Vector2.one;

                MapItemMenu mapItemMenuLesson = itemLesson.GetComponent<MapItemMenu>();
                mapItemMenuLesson.txtMenuItem.text = materialSubthemes[i].lessons[j].lessonName;
                mapItemMenuLesson.btnAction.interactable = true;
                mapItemMenuLesson.btnAction.onClick.RemoveAllListeners();
                mapItemMenuLesson.btnAction.onClick.AddListener(() =>
                {
                    Singleton.Instance.SetUsageIndex(x,y);
                    networkManager.StartHost();
                });
            }

        }
    }

    public void InitRoomList()
    {
        //clear objct first
        txtClientTitle.text = "Tunggu pemandu memulai server ...........";
        btnClientStart.SetActive(false);

        btnBack.SetActive(true);
        roomUI.SetActive(true);
        networkDiscovery.StartDiscovery();
        Debug.Log("begin discovery");
    }

    public void DisplayRoomList(ServerResponse resp)
    {
        txtClientTitle.text = "Pemandu ditemukan";
        btnClientStart.SetActive(true);
        btnClientLabel.text = "Mulai Pembelajaran";
        Button button = btnClientStart.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            NetworkManager.singleton.StartClient(resp.uri);
        });

    }

    public void BackButton()
    {
        switch (appState)
        {
            case AppState.main:
                break;
            case AppState.petunjuk:
                NavigateTo("main");
                networkDiscovery.StopDiscovery();
                break;
            case AppState.selectMateri:
                NavigateTo("mode");
                break;
            case AppState.selectMode:
                NavigateTo("main");
                break;
        }
    }

    public void NavigateTo(string menu)
    {
        btnBack.SetActive(true);

        foreach (GameObject wholeObj in screens)
        {
            wholeObj.SetActive(false);
        }
        switch (menu)
        {
            case "main":
                btnBack.SetActive(false);
                break;
            case "petunjuk":
                appState = AppState.petunjuk;
                petunjukUI.SetActive(true);
                break;
            case "mode":
                appState = AppState.selectMode;
                modeUI.SetActive(true);
                break;
            case "server":
                appState = AppState.selectMateri;
                materiUI.SetActive(true);
                break;
            case "client":
                appState = AppState.petunjuk;
                InitRoomList();
                break;
            case "pengembang":
                pengembangUI.SetActive(true);
                appState = AppState.petunjuk;
                break;
            case "kuis":
                loadingScreen.SetActive(true);
                loadingScreen.SetActive(true);
                SceneManager.LoadScene("Quiz");
                break;
        }
    }

}

