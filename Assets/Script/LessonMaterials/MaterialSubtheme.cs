using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Matherials/SubthemeMaterial", order = 3)]
public class MaterialSubtheme : ScriptableObject
{
    public MaterialLesson[] lessons;
    public string subthemeName;
}
