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
    public int state = 0;
    public InputField passwordField;
    public Text deviceIpAddress, passwordTitle, btnClientLabel, txtClientTitle;
    public GameObject learningSelectScreen, passwordScreen;

    enum AppState { main, pengembang, petunjuk, selectMode, selectMateri, selectRoom };
    AppState appState;

    //game component
    GameObject[] screens;
    public GameObject modeUI, materiUI, pengembangUI, petunjukUI, roomUI;
    public GameObject loadingScreen, btnBack, btnClientStart;

    public NetworkDiscovery networkDiscovery;

    // Use this for initialization
    void Start()
    {
        XRSettings.enabled = false;
        deviceIpAddress.text = "Alamat IP: ";
        passwordField.inputType = InputField.InputType.Password;

        txtClientTitle.text = "Tunggu pemandu memulai server ...........";

        screens = new GameObject[] { modeUI, materiUI, pengembangUI, petunjukUI, roomUI };
        NavigateTo("main");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
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

    public void PromptPassword()
    {
        passwordScreen.SetActive(true);
    }

    public void SelectLearningModule()
    {

        if (passwordField.text == "1234")
        {
            learningSelectScreen.SetActive(true);
            passwordScreen.SetActive(false);
        }
        else
        {
            passwordTitle.text = "Password salah";
        }
    }

    public void GoToImage()
    {
        SceneManager.LoadScene("Stage");
    }

    public void GoToVideo()
    {
        SceneManager.LoadScene("StageVideo");
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

