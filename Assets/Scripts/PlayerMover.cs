using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    //The player mover stores information about movement in the player elements
    private PlayerElements player;

    private Rigidbody rb;
    private bool gravity;
    private Vector3 forceOfGravity;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();
        rb = GetComponent<Rigidbody>();
        player.thrustersOn = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.RotateAround(transform.position, transform.forward, player.roll);
        transform.RotateAround(transform.position, transform.up, player.yaw);
        transform.RotateAround(transform.position, transform.right, player.pitch);
    }
    

    private void OnTriggerEnter(Collider col)
    {
        //Gravity Generator
        if(col.gameObject.tag == "Gravity Generator")
        {
            Debug.Log("Enter");
            gravity = true;
            forceOfGravity = col.gameObject.GetComponent<GravityGeneratorElements>().forceOfGravity;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //Gravity Generator
        if (col.gameObject.tag == "Gravity Generator")
        {
            Debug.Log("Exit");
            gravity = false;
            forceOfGravity = new Vector3(0, 0, 0);
        }
    }

    //The accelerate function is used to accelerate the player in any direction
    //The vector passed in represents the direction that the player is to be accelerated in
    //X is side to side, Y is up and down, and Z is forward
    //Right now this is only called by the input manager, but anything that needs to move the player should call this
    public void accelerate(Vector3 direction)
    {
        Vector3 horizontalVector = transform.right * direction.x;
        Vector3 verticalVector = transform.up * direction.y;
        Vector3 forwardVector = transform.forward * direction.z;

        player.movementVector = (horizontalVector + verticalVector + forwardVector).normalized;
        rb.AddForce((horizontalVector + verticalVector + forwardVector) *player.thrusterForce);
    }


    
}
