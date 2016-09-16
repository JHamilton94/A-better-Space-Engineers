using UnityEngine;
using System.Collections;

public class DebugMenuManager : MonoBehaviour {

    Canvas menuCanvas;
    bool menuOpen;

	// Use this for initialization
	void Start () {
        menuCanvas = GetComponentInChildren<Canvas>();
        menuOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        menuCanvas.gameObject.SetActive(menuOpen);
	}

    public void toggleMenu()
    {
        menuOpen = !menuOpen;
    }
}
