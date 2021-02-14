using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManager : MonoBehaviour
{
    public bool isSpinning;
    public float spinSpeed;
    public MaterialObject materialObject;
    public MapInfo[] mapInfos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isSpinning){
            this.transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed );
        }
    }
}

[System.Serializable]
public class MapInfo
{
    public BoxCollider collider;
    public string txtInfo;
    public UnityEvent OnReticleHover;
    public UnityEvent OnReticleHit;
}
