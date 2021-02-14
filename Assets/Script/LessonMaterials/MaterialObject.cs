using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Data", menuName = "Matherials/ObjectMaterial", order = 1)]
public class MaterialObject : ScriptableObject
{
    public string materialName;
    public enum MaterialType { Type3D, TypeVideo, TypeImage };
    public MaterialType materialType;
    public Material skyBox;
    public GameObject prefabObject;
    public VideoClip videoClip;

}

