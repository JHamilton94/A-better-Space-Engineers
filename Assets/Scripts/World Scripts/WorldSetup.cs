using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Net;

public class WorldSetup : MonoBehaviour {

    //Set in editor and then stored in WorldVariables, do not access these except through the editor
    public float MAX_SPEED;
    
    public void setWorldParameters(float MAX_SPEED)
    {
        WorldVariables.setGlobals(MAX_SPEED);
    }
}

