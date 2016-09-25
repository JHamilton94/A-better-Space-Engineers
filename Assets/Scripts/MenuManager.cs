using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System;

public class MenuManager : NetworkBehaviour
{

    public MyNetworkManager networkManager;

    public GameObject mainMenu;
    public GameObject hostMenu;
    public GameObject joinMenu;

    //Join Menu Fields
    public InputField ipField;
    public InputField portField;
    public Text errorText;

    public void Start()
    {
        networkManager = GetComponent<MyNetworkManager>();
        networkManager.offlineScene = "Main Menu";
    }

    public void quit()
    {
        Application.Quit();
    }

    public void gotoHostMenu()
    {
        mainMenu.SetActive(false);
        joinMenu.SetActive(false);

        hostMenu.SetActive(true);
    }

    public void gotoJoinMenu()
    {
        mainMenu.SetActive(false);
        hostMenu.SetActive(false);

        joinMenu.SetActive(true);

        errorText.enabled = false;
    }

    public void gotoMainMenu()
    {
        hostMenu.SetActive(false);
        joinMenu.SetActive(false);

        mainMenu.SetActive(true);
    }

    public void hostScene(int index)
    {
        WorldVariables.isServer = true;
        WorldVariables.playerCount = 1;
        WorldVariables.worldNum = index;
        WorldVariables.MAX_SPEED = 50;

        //setup server
        networkManager.onlineScene = "World " + index;

        //start server
        networkManager.StartHost();
    }

    public void joinScene()
    {
        int port;

        //Resolve Port
        if (portField.text != "")
        {
            port = int.Parse(portField.text);
        }
        else
        {
            port = 5000;
        }

        Debug.Log(ipField.text);
        
        networkManager.networkAddress = ipField.text;
        networkManager.networkPort = port;
        networkManager.StartClient();
        
    }
    
}
