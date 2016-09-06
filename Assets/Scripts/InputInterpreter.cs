using UnityEngine;
using System.Collections;

public class InputInterpreter : MonoBehaviour {

    private float yaw;
    private float pitch;
    private float roll;

    private PlayerMover mover;
    private PlayerElements player;

	// Use this for initialization
	void Start () {
        yaw = 0;
        pitch = 0;
        roll = 0;

        mover = GetComponent<PlayerMover>();
        player = GetComponent<PlayerElements>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        yaw += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse X");
        pitch += -GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse Y");
        roll += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Roll");


        transform.eulerAngles = new Vector3(pitch, yaw, roll);


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
    }

}
