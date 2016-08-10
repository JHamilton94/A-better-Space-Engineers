using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour {

    private PlayerElements player;
    public Text thrustersIndicator;

	// Use this for initialization
	void Start () {
        thrustersIndicator.text = "ASFDFBFD";
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerElements>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player.thrustersOn)
        {
            thrustersIndicator.text = "ON";
        }
        else
        {
            thrustersIndicator.text = "OFF";
        }
	}
}
