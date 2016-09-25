using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WorldSpawner : MonoBehaviour {

    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
	
	}


    public void spawnPlayer(NetworkConnection conn)
    {
        GameObject player = Instantiate(playerPrefab);

        Debug.Log(conn.isConnected);

        NetworkServer.AddPlayerForConnection(conn, player, 0);
    }
    	
}
