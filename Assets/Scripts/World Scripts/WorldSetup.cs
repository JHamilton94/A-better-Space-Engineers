using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Net;

public class WorldSetup : NetworkBehaviour {
    public bool setWorldVariablesFromEditor;

    //Set in editor and then stored in WorldVariables, do not access these except through the editor
    public GameObject playerPrefab;
    public float MAX_SPEED;
    public int playerCount;
    public int worldNum;

    private WorldSpawner worldSpawner;

    private Camera defaultCamera;

    

    //Sets up the scene, always called first
    public void Start()
    {
        worldSpawner = GetComponent<WorldSpawner>();

        defaultCamera = GetComponent<Camera>();
        defaultCamera.enabled = false;

        //Setup world variables
        if (setWorldVariablesFromEditor) {
            
            WorldVariables.setGlobals(MAX_SPEED, playerCount, worldNum, isServer);
        }
    }
}

