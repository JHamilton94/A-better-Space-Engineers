using System.Net;
using UnityEngine.Networking;
using System.Collections.Generic;

public static class WorldVariables{

    public static float MAX_SPEED;
    public static int playerCount;
    public static int worldNum;
    public static bool isServer;

    //Accessed once at the beginning of the scene on start, should not be accessed ever again. Made to allow editor access to global variables.
    public static void setGlobals(float MAX_SPEED, int playerCount, int worldNum, bool isServer)
    {
        WorldVariables.MAX_SPEED = MAX_SPEED;
        WorldVariables.playerCount = playerCount;
        WorldVariables.worldNum = worldNum;
        WorldVariables.isServer = isServer;
    }



}
