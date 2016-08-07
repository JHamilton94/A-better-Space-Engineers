using UnityEngine;
using System.Collections;

public class InputInterpreter : MonoBehaviour {

    private float yaw;
    private float pitch;
    private float roll;

    private PlayerMover mover;

	// Use this for initialization
	void Start () {
        yaw = 0;
        pitch = 0;
        roll = 0;

        mover = GetComponent<PlayerMover>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        yaw += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse X");
        pitch += -GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse Y");
        //roll += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(pitch, yaw, roll);


        //Interpret movement
        if (Input.GetAxis("Forward") > 0)
        {
            mover.accelerateForward();
        }
        if(Input.GetAxis("Forward") < 0)
        {
            mover.accelerateBackwards();
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            mover.accelerateRight();
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            mover.accelerateLeft();
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            mover.accelerateUp();
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            mover.accelerateDown();
        }

        //Dampening goes here
    }

}
