using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System;

public class MenuManager : NetworkBehaviour
{

    public CustomNetworkManager networkManager;
    public WorldSetup worldSetup;

    public GameObject mainMenu;
    public GameObject hostMenu;
    public GameObject joinMenu;

    //Join Menu Fields
    public InputField ipField;
    public InputField portField;
    public Text errorText;

    public void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<CustomNetworkManager>();
        worldSetup = GameObject.FindGameObjectWithTag("Manager").GetComponent<WorldSetup>();
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
        worldSetup.setWorldParameters(10.0f);
        networkManager.HostServer("192.168.0.161", 5000, "World " + index);
    }

    public void joinScene()
    {
        string ip;
        int port;
        if (portField.text != "")
        {
            port = int.Parse(portField.text);
        }
        else
        {
            //Default port
            port = 5000;
        }

        //resolves dns to ip
        ip = Dns.GetHostAddresses(ipField.text)[0].ToString();

        networkManager.JoinServer(ip, port);
    }
    
}
