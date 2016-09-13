using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    //The player mover stores information about movement in the player elements
    private PlayerElements player;

    private Rigidbody rb;
    private Mesh mesh;
    private Camera camera;

    //Transition states
    private bool rotatingUpright;
    private bool clampRoll; //Clamps the roll rotation

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();

        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshFilter>().mesh;
        camera = GetComponentInChildren<Camera>();

        player.thrustersOn = true;

        player.height = mesh.bounds.size.y;

        rotatingUpright = false;
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
                camera.transform.RotateAround(camera.transform.position, camera.transform.forward, player.roll);
                camera.transform.RotateAround(camera.transform.position, camera.transform.up, player.yaw);
                camera.transform.RotateAround(camera.transform.position, camera.transform.right, player.pitch);

                //hacky
                camera.transform.parent = null;
                Quaternion desiredRotation = Quaternion.FromToRotation(transform.forward, camera.transform.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation * transform.rotation, 1);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, camera.transform.eulerAngles.z);
                camera.transform.parent = gameObject.transform;

                break;
            case MovementState.Floating: //Cant orient self without thrusters
                break;
            case MovementState.Walking://Orient transform to gravity, orient camera roll and yaw to transform, camera pitch is independent
                //Move Camera freely
                float pitch = player.pitch + camera.transform.localEulerAngles.x;
                float yaw = player.yaw + transform.localEulerAngles.y;
                camera.transform.localEulerAngles = new Vector3(pitch,0,0);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);

                //Orient character to upright
                Quaternion uprightRotation = Quaternion.FromToRotation(transform.up, -player.forceOfGravity);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, uprightRotation * transform.rotation, 1);

                //yaw player towards camera
                
                
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
        Gizmos.DrawLine(transform.position, transform.position + player.forceOfGravity.normalized * (player.height/2 + 1f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.up.normalized * (player.height/2 + 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.DrawLine(transform.position, transform.position + camera.transform.forward);
    }
    
}
