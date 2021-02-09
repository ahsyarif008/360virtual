using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "Matherials/ObjectMaterial", order = 1)]
public class MaterialObject : ScriptableObject
{
    public string materialName;
    public enum MaterialType { Type3D, TypeVideo, TypeImage };
    public MaterialType materialType;
    public MapInfo[] mapInfos;

}

[System.Serializable]
public class MapInfo
{
    public BoxCollider collider;
    public string txtInfo;
    public UnityEvent OnReticleHover;
    public UnityEvent OnReticleHit;
}