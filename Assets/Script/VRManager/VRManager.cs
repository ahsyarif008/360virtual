using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VRManager : MonoBehaviour
{


    [SerializeField] MaterialObject[] lessonMaterials;
    [SerializeField] GameObject teacherGUI;
    [SerializeField] Transform parentTeacherInfo;
    [SerializeField] GameObject prefabInfoItem;
    // Start is called before the first frame update
    void Start()
    {
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
    void StartMaterials(int index){
        //registeredMaterials
        MaterialObject selectedMaterial = lessonMaterials[index];

        Debug.Log("starting lesson " + selectedMaterial.materialName);
    }


}
