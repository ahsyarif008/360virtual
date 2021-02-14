using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapItemInfo : MonoBehaviour
{
    public delegate void OnStartLesson(int index);
    public static OnStartLesson StartLesson;
    public int materialIndex;
    float sliderValue = 0;
    float intervalValue = 0.01f;
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
            sliderValue += intervalValue;
        }
        sliderValue = 0;
        StartMaterials();
    }

    void StartMaterials()
    {
        StartLesson?.Invoke(materialIndex);
    }

}
