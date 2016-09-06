using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    private PlayerElements player;
    private Rigidbody rb;
    private bool gravity;
    private Vector3 forceOfGravity;

    private List<GameObject> gravGenerators;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();
        rb = GetComponent<Rigidbody>();
        player.thrustersOn = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
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

    public void accelerate(Vector3 direction)
    {
        Vector3 horizontalVector = transform.right * direction.x;
        Vector3 verticalVector = transform.up * direction.y;
        Vector3 forwardVector = transform.forward * direction.z;

        player.movementVector = (horizontalVector + verticalVector + forwardVector).normalized;
        rb.AddForce((horizontalVector + verticalVector + forwardVector) *player.thrusterForce);

    }
    
}
