using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Data", menuName = "Matherials/ObjectMaterial", order = 1)]
public class MaterialObject : ScriptableObject
{
    public string materialName;
    [TextArea]
    public string description;
    public enum MaterialType { Type3D, TypeVideo, TypeImage, TypeImageWithPinpoint, Type360Video, TypeMusic, TypeCursor, Type3DWithVideoSkybox };
    public MaterialType materialType;
    public Sprite spriteImage;
    public Material skyBox;
   // public RenderTexture skyBoxRenderTex;
    public GameObject prefabObject;
    public VideoClip videoClip;
    public AudioClip audioClip;

}

