using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    //The player mover stores information about movement in the player elements
    private PlayerElements player;

    private Rigidbody rb;
    private Vector3 forceOfGravity;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();
        rb = GetComponent<Rigidbody>();
        player.thrustersOn = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Update movement state
        if (player.thrustersOn)
        {
            player.movementState = MovementState.Flying;
        } else if (player.forceOfGravity != new Vector3(0,0,0))
        {
            player.movementState = MovementState.Walking;
        } else
        {
            player.movementState = MovementState.Floating;
        }

        transform.RotateAround(transform.position, transform.forward, player.roll);
        transform.RotateAround(transform.position, transform.up, player.yaw);
        transform.RotateAround(transform.position, transform.right, player.pitch);
    }
    

    private void OnTriggerEnter(Collider col)
    {
        //Gravity Generator
        if(col.gameObject.tag == "Gravity Generator")
        {
            player.forceOfGravity = col.gameObject.GetComponent<GravityGeneratorElements>().forceOfGravity;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //Gravity Generator
        if (col.gameObject.tag == "Gravity Generator")
        {
            player.forceOfGravity = new Vector3(0, 0, 0);
        }
    }

    //The accelerate function is used to accelerate the player in any direction
    //The vector passed in represents the direction that the player is to be accelerated in
    //X is side to side, Y is up and down, and Z is forward
    //Right now this is only called by the input manager, but anything that needs to move the player should call this
    public void accelerate(Vector3 direction)
    {
        switch (player.movementState)
        {
            case MovementState.Flying:
                Vector3 horizontalVector = transform.right * direction.x;
                Vector3 verticalVector = transform.up * direction.y;
                Vector3 forwardVector = transform.forward * direction.z;

                player.movementVector = (horizontalVector + verticalVector + forwardVector).normalized;
                rb.AddForce((horizontalVector + verticalVector + forwardVector) * player.thrusterForce);
                break;
            case MovementState.Floating:
                //do nothing
                break;
            case MovementState.Walking:
                //TODO impliment walking
                break;
        }
        
    }


    
}
