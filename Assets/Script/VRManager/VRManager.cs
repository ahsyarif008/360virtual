using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR;
using Mirror;

public class VRManager : NetworkBehaviour
{
    [SerializeField] GameObject teacherGUI;
    [SerializeField] GameObject tvObject;
    [SerializeField] GameObject radioObject;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] GameObject planeImage;
    [SerializeField] Transform parentTeacherInfo;
    [SerializeField] Transform object3DPos;
    [SerializeField] GameObject prefabInfoItem;
    [SerializeField] GameObject classItem;

    [Header("Render textures")]
    [SerializeField] RenderTexture renderTexTelevision;
    [SerializeField] RenderTexture renderTexSkybox;

    [SyncVar(hook = nameof(SetMaterialIndex))] int currentMaterialIndex = 100;
    [SyncVar(hook = nameof(SetupCurrentIndexSubtheme))] int indexSubtheme = 0;
    [SyncVar(hook = nameof(SetupCurrentIndexLesson))] int indexLesson = 0;


    GameObject object3D;
    // Start is called before the first frame update

    public override void OnStartServer()
    {
        base.OnStartServer();

        SetupTeacherGUIPanel();
        InitialClient();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        InitialClient();
    }


    void InitialClient()
    {
        XRSettings.enabled = true;
        tvObject.SetActive(false);
        RenderSettings.skybox = null;
    }

    void OnEnable()
    {
        MapItemInfo.StartLesson += StartMaterials;
    }

    void OnDestroy()
    {
        MapItemInfo.StartLesson -= StartMaterials;
    }

    void SetupCurrentIndexSubtheme(int oldNum, int newNum)
    {
        Debug.Log("called sub");
        indexSubtheme = newNum;
    }
    void SetupCurrentIndexLesson(int oldNum, int newNum)
    {
        Debug.Log("called lesson");
        indexLesson = newNum;
    }

    void Update()
    {
        //cheat sheet
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentMaterialIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentMaterialIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentMaterialIndex = 2;
        }
    }

    void SetupTeacherGUIPanel()
    {
        indexSubtheme = Singleton.Instance.indexSubtheme;
        indexLesson = Singleton.Instance.indexLesson;

        Debug.Log(indexSubtheme + "," + indexLesson);

        //loop through materials
        for (int i = 0; i < Singleton.Instance.materialSubthemes[indexSubtheme].lessons[indexLesson].materials.Length; i++)
        {
            //instantiate initialize
            GameObject item = Instantiate(prefabInfoItem, Vector3.zero, transform.rotation, parentTeacherInfo);
            item.transform.localPosition = Vector3.zero;
            item.transform.rotation = new Quaternion(0, 0, 0, 0);

            //caching object
            MapItemInfo itemInfo = item.GetComponent<MapItemInfo>();
            TMP_Text txtInfo = itemInfo.txtInfo;

            //assign value
            txtInfo.text = Singleton.Instance.materialSubthemes[indexSubtheme].lessons[indexLesson].materials[i].materialName;
            itemInfo.materialIndex = i;

        }

        teacherGUI.SetActive(false);
    }

    public void SelectingLesson(Collider2D targetLesson)
    {
        MaterialObject material = targetLesson.GetComponent<MaterialObject>();
        var type = material.materialType;
    }

    void SetMaterialIndex(int oldIndex, int newIndex)
    {
        //  StartMaterials(newIndex);
        //after this code is executed, due to syncVar, start materials function will be executed
    }


    //public void BeginLesson
    void StartMaterials(int index)
    {
        currentMaterialIndex = index;
        ResetBehaviorToDefault();

        //registeredMaterials
        MaterialObject selectedMaterial = Singleton.Instance.materialSubthemes[indexSubtheme].lessons[indexLesson].materials[currentMaterialIndex];

        //setup skybox
        if (selectedMaterial.skyBox != null)
        {
            RenderSettings.skybox = selectedMaterial.skyBox;
            classItem.SetActive(false);
        }
        else
        {
            RenderSettings.skybox = null;
            classItem.SetActive(true);
        }

        //switch by material type
        switch (selectedMaterial.materialType)
        {
            case MaterialObject.MaterialType.Type3D:
                object3D = Instantiate(selectedMaterial.prefabObject, object3DPos.position, Quaternion.identity);
                //setup interactions here :

                break;
            case MaterialObject.MaterialType.TypeVideo:
                tvObject.SetActive(true);
                videoPlayer.targetTexture = renderTexTelevision;
                videoPlayer.clip = selectedMaterial.videoClip;
                videoPlayer.Play();

                break;
            case MaterialObject.MaterialType.TypeImage:
                planeImage.SetActive(true);
                planeImage.GetComponent<Image>().sprite = selectedMaterial.spriteImage;
                //  planeImage.GetComponent<Renderer>().material.SetTexture("_MyTexture", selectedMaterial.textureImage);
                break;

            case MaterialObject.MaterialType.Type360Video:
                //    videoPlayer.targetTexture = selectedMaterial.skyBoxRenderTex;
                videoPlayer.targetTexture = renderTexSkybox;
                videoPlayer.clip = selectedMaterial.videoClip;
                videoPlayer.Play();
                break;
            case MaterialObject.MaterialType.TypeMusic:
                radioObject.SetActive(true);
                audioPlayer.clip = selectedMaterial.audioClip;
                audioPlayer.Play();
                break;
        }
    }

    
    void ResetBehaviorToDefault()
    {
        Destroy(object3D);
        tvObject.SetActive(false);
        radioObject.SetActive(false);
        planeImage.SetActive(false);
        videoPlayer.Stop();
        audioPlayer.Stop();
        teacherGUI.SetActive(false);
    }


}
