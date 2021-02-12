using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class VRManager : MonoBehaviour
{
    [SerializeField] MaterialObject[] lessonMaterials;
    [SerializeField] GameObject teacherGUI;
    [SerializeField] GameObject tvObject;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Transform parentTeacherInfo;
    [SerializeField] Transform object3DPos;
    [SerializeField] GameObject prefabInfoItem;

    GameObject object3D;
    // Start is called before the first frame update
    void Start()
    {
        tvObject.SetActive(false);
        RenderSettings.skybox = null;

        SetupTeacherGUIPanel();
    }

    void OnEnable()
    {
        MapItemInfo.StartLesson += StartMaterials;
    }

    void OnDestroy()
    {
        MapItemInfo.StartLesson -= StartMaterials;
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

    //public void BeginLesson
    void StartMaterials(int index)
    {

        //registeredMaterials
        MaterialObject selectedMaterial = lessonMaterials[index];

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
