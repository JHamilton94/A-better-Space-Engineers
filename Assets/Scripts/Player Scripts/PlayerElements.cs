using UnityEngine;
using System.Collections;

public class PlayerElements : MonoBehaviour {

    //Player info
    public float width;
    public float height;
    public MovementState movementState;
    public float playerSpeed;
    public Vector3 movementVector;
    public float walkingSpeed;
    
    //Player Gravity Info
    public Vector3 forceOfGravity;
    public bool grounded;
    public float reactionWheelTorque;

    //Player systems info
    public bool thrustersOn;
    public float thrusterForce;

    //Player Orientation
    public float yaw;
    public float pitch;
    public float roll;

    //should be private but fuck it
    public float forwardAxismagnitude;
    public float horizontalAxismagnitude;
    public float verticalAxisMagnitude;

    //Debug states
    public bool noClip;

    public void toggleThrusters()
    {
        thrustersOn = !thrustersOn;
    }
}
