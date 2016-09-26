using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager {

    public Text invalidIPError;

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        invalidIPError.enabled = true;
        base.OnClientError(conn, errorCode);
    }

    

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Spawning player...");
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, 0);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Client disconected: " + conn.address);
        base.OnClientDisconnect(conn);
    }


    public void HostServer(string address, int port, string world)
    {
        networkAddress = address;
        networkPort = port;
        onlineScene = world;
        
        StartHost();
    }

    public void JoinServer(string address, int port)
    {
        networkAddress = address;
        networkPort = port;

        StartClient();
    }
}
