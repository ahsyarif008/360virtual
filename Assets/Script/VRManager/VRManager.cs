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

    [SerializeField] MaterialObject[] lessonMaterials;
    [SerializeField] GameObject teacherGUI;
    [SerializeField] GameObject tvObject;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Transform parentTeacherInfo;
    [SerializeField] Transform object3DPos;
    [SerializeField] GameObject prefabInfoItem;
    [SerializeField] GameObject classItem;

    [SyncVar(hook = nameof(StartMaterials))] int currentMaterialIndex = 100;

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
        MapItemInfo.StartLesson += SetMaterialIndex;
    }

    void OnDestroy()
    {
        MapItemInfo.StartLesson -= SetMaterialIndex;
    }

    void SetupTeacherGUIPanel()
    {
        //loop through materials
        for (int i = 0; i < lessonMaterials.Length; i++)
        {
            //instantiate initialize
            GameObject item = Instantiate(prefabInfoItem, Vector3.zero, transform.rotation, parentTeacherInfo);
            item.transform.localPosition = Vector3.zero;
            item.transform.rotation = new Quaternion(0, 0, 0, 0);

            //caching object
            MapItemInfo itemInfo = item.GetComponent<MapItemInfo>();
            TMP_Text txtInfo = itemInfo.txtInfo;

            //assign value
            txtInfo.text = lessonMaterials[i].materialName;
            itemInfo.materialIndex = i;

        }
    }

    public void SelectingLesson(Collider2D targetLesson)
    {
        MaterialObject material = targetLesson.GetComponent<MaterialObject>();
        var type = material.materialType;
    }

    void SetMaterialIndex(int index)
    {
        currentMaterialIndex = index;

        //after this code is executed, due to syncVar, start materials function will be executed
    }


    //public void BeginLesson
    void StartMaterials(int oldIndex, int newIndex)
    {
        //registeredMaterials
        MaterialObject selectedMaterial = lessonMaterials[currentMaterialIndex];

        //setup skybox
        if (selectedMaterial.skyBox != null)
        {
            RenderSettings.skybox = selectedMaterial.skyBox;
        }



        switch (selectedMaterial.materialType)
        {
            case MaterialObject.MaterialType.Type3D:
                object3D = Instantiate(selectedMaterial.prefabObject, object3DPos.position, Quaternion.identity);
                //setup interactions

                break;
            case MaterialObject.MaterialType.TypeVideo:
                tvObject.SetActive(true);
                videoPlayer.clip = selectedMaterial.videoClip;
                videoPlayer.Play();

                break;
            case MaterialObject.MaterialType.TypeImage:

                break;
        }
    }


}
