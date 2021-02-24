using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Direction : MonoBehaviour
{
    [SerializeField] GameObject[] contents;
    [SerializeField] Button btnNext;
    [SerializeField] Button btnPrev;
    int indexCurrDir;

    void Start()
    {
        btnNext.onClick.AddListener(NextDirection);
        btnPrev.onClick.AddListener(PrevDirection);

        contents[indexCurrDir].SetActive(true);
        CheckBtnVisibility();
    }

    public void NextDirection()
    {
        if (indexCurrDir < (contents.Length - 1))
        {
            indexCurrDir++;
            DisableAllContent();
            contents[indexCurrDir].SetActive(true);
            CheckBtnVisibility();
        }
    }

    public void PrevDirection()
    {
        if (indexCurrDir > 0)
        {
            indexCurrDir--;
            DisableAllContent();
            contents[indexCurrDir].SetActive(true);
            CheckBtnVisibility();
        }
    }

    //Start is called before the first frame update//Start is called before the first frame update
    void DisableAllContent()
    {
        foreach (GameObject obj in contents)
        {
            obj.SetActive(false);
        }
    }

    void CheckBtnVisibility()
    {
        btnPrev.gameObject.SetActive(true);
        btnNext.gameObject.SetActive(true);
        if (indexCurrDir == 0)
        {
            btnPrev.gameObject.SetActive(false);
        }
        else if (indexCurrDir == (contents.Length - 1))
        {
            btnNext.gameObject.SetActive(false);
        }
    }

}
