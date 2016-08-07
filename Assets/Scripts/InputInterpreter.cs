using UnityEngine;
using System.Collections;

public class InputInterpreter : MonoBehaviour {

    private float yaw;
    private float pitch;
    private float roll;

	// Use this for initialization
	void Start () {
        yaw = 0;
        pitch = 0;
        roll = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        yaw += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse X");
        pitch += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse Y");
        //roll += GlobalVariables.MOUSE_SENSITIVITY * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(pitch, yaw, roll);
    }
}
