using UnityEngine;
using System.Collections;

public class PlayerElements : MonoBehaviour {

    public float playerWidth;
    public float playerheight;
    public float playerAcceleration;
    public float maxSpeed;
    public float playerSpeed;
    public Vector3 movementVector;

    public bool thrustersOn;

    //should be private but fuck it
    public float forwardAxismagnitude;
    public float horizontalAxismagnitude;
    public float verticalAxisMagnitude;

    public void toggleThrusters()
    {
        thrustersOn = !thrustersOn;
    }
}
