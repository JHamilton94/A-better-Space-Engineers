using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class InputInterpreter : NetworkBehaviour {

    private float yaw;
    private float pitch;
    private float roll;

    private PlayerMover mover;
    private PlayerElements player;
    private DebugMenuManager debugMenu;

    private Camera camera;

	// Use this for initialization
	void Start () {

        //Networking player setup
        camera = GetComponentInChildren<Camera>();
        if (!isLocalPlayer)
        {
            camera.enabled = false;
            return;
        }

        mover = GetComponent<PlayerMover>();
        player = GetComponent<PlayerElements>();
        debugMenu = GetComponent<DebugMenuManager>();

        player.yaw = 0;
        player.pitch = 0;
        player.roll = 0;
    }

    // Update is called once per frame
    void Update() {

        if (!isLocalPlayer)
        {
            return;
        }

        player.yaw = GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse X");
        player.pitch = -GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse Y");
        player.roll = GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Roll");


        //transform.eulerAngles = new Vector3(pitch, yaw, roll);


        //Interpret movement
        Vector3 directionVector = new Vector3(0,0,0);
        if (Input.GetAxis("Forward") > 0)
        {
            directionVector += new Vector3(0,0,1);
        }
        if (Input.GetAxis("Forward") < 0)
        {
            directionVector += new Vector3(0, 0, -1);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            directionVector += new Vector3(1, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            directionVector += new Vector3(-1, 0, 0);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            directionVector += new Vector3(0, 1, 0);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            directionVector += new Vector3(0, -1, 0);
        }

        mover.accelerate(directionVector);

        if (Input.GetButtonDown("ToggleThrusters"))
        {
            Debug.Log("Toggling");
            player.toggleThrusters();
            Debug.Log("Done");
        }

        //Dampening goes here

        //Interpret Shooting
        if (Input.GetButton("fire1"))
        {
            shoot();
        }


        //Debugging menu
        if (Input.GetButtonDown("OpenDebugMenu"))
        {
            debugMenu.toggleMenu();
        }
    }

    public void shoot()
    {

    }

}
