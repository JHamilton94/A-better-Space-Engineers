using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    //The player mover stores information about movement in the player elements
    private PlayerElements player;

    private Rigidbody rb;
    private Mesh mesh;

    private bool clampRoll; //Clamps the roll rotation

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();

        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshFilter>().mesh;
        
        player.thrustersOn = true;

        player.height = mesh.bounds.size.y;

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

        //Orient Player
        switch (player.movementState)
        {
            case MovementState.Flying: //Orient everything to the camera
                transform.RotateAround(transform.position, transform.forward, player.roll);
                transform.RotateAround(transform.position, transform.up, player.yaw);
                transform.RotateAround(transform.position, transform.right, player.pitch);
                break;
            case MovementState.Floating: //Cant orient self without thrusters
                break;
            case MovementState.Walking://Orient transform to gravity, orient camera roll and yaw to transform, camera pitch is independent
                //Roll towards grav vector
                Vector3 crossProduct = Vector3.Cross(transform.up, player.forceOfGravity);
                float dot = Vector3.Dot(crossProduct, new Vector3(0,0,1));
                int sign = 0;
                if(dot > 0) // rotate left
                {
                    sign = -1;
                }else if (dot < 0) // rotate right
                {
                    sign = 1;
                }
                else // no rotation
                {
                    sign = 0;
                    Debug.Log("Perfect");
                }
                
                //stop rotation
                if(Vector3.Angle(transform.up, player.forceOfGravity) < 1)
                {
                    player.roll = Vector3.Angle(transform.up, player.forceOfGravity) * sign;
                }
                else
                {
                    player.roll = 1 * sign;
                    
                }
                
                //find direction to rotate
                transform.RotateAround(transform.position, transform.forward, player.roll);
                
                break;
        }
        

        //Apply gravity
        switch (player.movementState)
        {
            case MovementState.Flying:
                //do nothing
                player.grounded = false;
                break;
            case MovementState.Floating:
                //do nothing
                player.grounded = false;
                break;
            case MovementState.Walking:
                player.grounded = detectGround();
                if (!player.grounded)
                {
                    applyGravity();
                }
                break;
        }

        player.grounded = detectGround();

        
    }
    
    private void orientPlayer()
    {
        

    }

    private bool detectGround()
    {
        Physics.Raycast(transform.position, player.forceOfGravity, player.height/2 + 0.1f);
        return true;
    }

    private void applyGravity()
    {

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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + player.forceOfGravity.normalized * (player.height/2 + 0.1f));
    }
    
}
