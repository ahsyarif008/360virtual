using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Matherials/LessonMaterial", order = 2)]
public class MaterialLesson : ScriptableObject
{
    public MaterialObject[] materials;
    public string lessonName;

}
