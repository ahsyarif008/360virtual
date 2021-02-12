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
    public MapInfo[] mapInfos;
    public Material skyBox;
    public GameObject prefabObject;
    public VideoClip videoClip;

}

[System.Serializable]
public class MapInfo
{
    public BoxCollider collider;
    public string txtInfo;
    public UnityEvent OnReticleHover;
    public UnityEvent OnReticleHit;
}