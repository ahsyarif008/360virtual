using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Mirror;

public class MapDeskMenu : MonoBehaviour
{
    public enum DeskMenuType { OpenMenu, BackToMain }
    public DeskMenuType deskMenuType;

    [SerializeField] GameObject teacherPanel;
    float sliderValue = 0;
    float intervalValue = 0.01f;
    public float addedValue = 0.01f;
    public TMP_Text txtInfo;
    public Slider sliderInfo;

    public void OnPointerHover()
    {
        StartCoroutine(LoadingContent());
    }

    public void OnPointerUnhover()
    {
        StopAllCoroutines();
        sliderValue = 0;
        sliderInfo.value = sliderValue;
    }

    IEnumerator LoadingContent()
    {
        while (sliderValue < 1)
        {
            yield return new WaitForSeconds(intervalValue);
            sliderInfo.value = sliderValue;
            sliderValue += addedValue;
        }
        if (deskMenuType == DeskMenuType.OpenMenu)
            TogglePanelMenu(true);
        else
            BackToMain();

    }

    public void TogglePanelMenu(bool activeStat)
    {
        teacherPanel.SetActive(activeStat);
    }

    public void BackToMain()
    {
        MyNetworkManager networkManager = Singleton.Instance.networkManager;
        networkManager.StopHost();

        SceneManager.LoadScene("Title");

    }


}
