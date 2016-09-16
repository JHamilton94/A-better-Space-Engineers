﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    //The player mover stores information about movement in the player elements
    private PlayerElements player;

    private Rigidbody rb;
    private Mesh mesh;
    private Camera camera;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();

        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshFilter>().mesh;
        camera = GetComponentInChildren<Camera>();

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
            case MovementState.Flying:
                orientFlyingPlayer();
                break;
            case MovementState.Floating:
                orientFloatingPlayer();
                break;
            case MovementState.Walking:
                orientWalkingPlayer();
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
                applyGravity();
                break;
        }
        
    }
    
    private void orientFlyingPlayer()
    {
        Quaternion desiredRotation;
        camera.transform.RotateAround(camera.transform.position, camera.transform.forward, player.roll);
        camera.transform.RotateAround(camera.transform.position, camera.transform.right, player.pitch);
        camera.transform.RotateAround(camera.transform.position, camera.transform.up, player.yaw);


        //hacky 
        camera.transform.parent = null;

        //Prevent the body from snapping to a rotation, and allows a more gradual transition to flying
        desiredRotation = Quaternion.FromToRotation(transform.forward, camera.transform.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation * transform.rotation, player.reactionWheelTorque);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, camera.transform.eulerAngles.z);

        camera.transform.parent = gameObject.transform;
        //end of hack
    }

    private void orientWalkingPlayer()
    {
        Quaternion desiredRotation;
        //Move Camera freely
        float pitch = player.pitch + camera.transform.localEulerAngles.x;
        float yaw = player.yaw + transform.localEulerAngles.y;
        camera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);

        //Orient character to upright
        desiredRotation = Quaternion.FromToRotation(transform.up, -player.forceOfGravity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation * transform.rotation, player.reactionWheelTorque);
        
    }

    private void orientFloatingPlayer()
    {
        Quaternion desiredRotation;
        camera.transform.RotateAround(camera.transform.position, camera.transform.forward, player.roll);
        camera.transform.RotateAround(camera.transform.position, camera.transform.up, player.yaw);
        camera.transform.RotateAround(camera.transform.position, camera.transform.right, player.pitch);

        //hacky
        camera.transform.parent = null;
        desiredRotation = Quaternion.FromToRotation(transform.forward, camera.transform.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation * transform.rotation, player.reactionWheelTorque);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, camera.transform.eulerAngles.z);
        camera.transform.parent = gameObject.transform;
        //end of hack
    }

    private RaycastHit detectGround()
    {
        Ray groundCheck = new Ray(transform.position, player.forceOfGravity.normalized);
        RaycastHit hitInformation;
        Physics.Raycast(groundCheck, out hitInformation, player.height/2 + 0.1f);
        return hitInformation;
    }

    private void applyGravity()
    {
        
        RaycastHit grounded = detectGround();
        Debug.Log(grounded);
        if (grounded.collider == null)
        {
            rb.AddForce(player.forceOfGravity, ForceMode.Impulse);
        }
        else
        {
            //set position to just above the ground
        }
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
        /*Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + player.forceOfGravity.normalized * (player.height/2 + 1f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.up.normalized * (player.height/2 + 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.DrawLine(transform.position, transform.position + camera.transform.forward);*/
    }
    
}