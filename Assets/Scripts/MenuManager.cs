using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject hostMenu;
    public GameObject joinMenu;

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
    }

    public void gotoMainMenu()
    {
        hostMenu.SetActive(false);
        joinMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
    
}
