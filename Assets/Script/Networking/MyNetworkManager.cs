using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class MyNetworkManager : NetworkManager
{

    public NetworkDiscovery networkDiscovery;
    public override void StartHost()
    {
        base.StartHost();
        networkDiscovery.AdvertiseServer();
        Debug.Log("advertise server");
    }

}
