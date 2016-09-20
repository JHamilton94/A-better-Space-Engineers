using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugMenuManager : MonoBehaviour {

    //Menu elements
    Canvas menuCanvas;
    Toggle gravity;

    //Menu status
    bool menuOpen;

    //Data sources
    PlayerElements player;

	// Use this for initialization
	void Start () {
        menuCanvas = GetComponentInChildren<Canvas>();
        gravity = GetComponentInChildren<Toggle>();

        menuOpen = false;

        player = GetComponentInParent<PlayerElements>();
    }
	
	// Update is called once per frame
	void Update () {
        menuCanvas.gameObject.SetActive(menuOpen);

        //update status indicators
        gravity.isOn = player.forceOfGravity != new Vector3(0, 0, 0);

        //do other things?
	}

    public void toggleMenu()
    {
        menuOpen = !menuOpen;
    }
}
