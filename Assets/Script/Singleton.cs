using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class Singleton : MonoBehaviour
{

    public static Singleton Instance = null;
    public MyNetworkManager networkManager;
    public MyNetworkDiscovery networkDiscovery;
    public MaterialSubtheme[] materialSubthemes;
    public int indexSubtheme;
    public int indexLesson;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;


        DontDestroyOnLoad(this.gameObject);
    }

    public void SetUsageIndex(int idxSubtheme, int idxLesson){
        indexSubtheme = idxSubtheme;
        indexLesson = idxLesson;

        Debug.Log("st " + indexSubtheme + "  leson " + indexLesson);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
