using UnityEngine;
using System.Collections;

public class PlayerElements : MonoBehaviour {

    public float playerWidth;
    public float playerheight;
    public MovementState movementState;
    public float playerAcceleration;
    public float maxSpeed;
    public float playerSpeed;
    public Vector3 movementVector;
    public float thrusterForce;

    public Vector3 forceOfGravity;

    public bool thrustersOn;

    public float yaw;
    public float pitch;
    public float roll;

    //should be private but fuck it
    public float forwardAxismagnitude;
    public float horizontalAxismagnitude;
    public float verticalAxisMagnitude;

    public void toggleThrusters()
    {
        thrustersOn = !thrustersOn;
    }
}
