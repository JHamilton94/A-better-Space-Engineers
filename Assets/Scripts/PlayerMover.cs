using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    private PlayerElements player;
    private bool gravity;
    private Vector3 forceOfGravity;

    private List<GameObject> gravGenerators;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();
        player.thrustersOn = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        movePlayer();
    }

    private void movePlayer()
    {

        if (!player.thrustersOn)
        {
            player.movementVector += forceOfGravity;
        }

        this.transform.position += player.movementVector;

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

    public void accelerateForward()
    {
        if (player.thrustersOn)
        {
            player.movementVector += this.transform.forward * 0.001f;
        }
        else if(gravity)
        {

        }

    }

    public void accelerateBackwards()
    {
        if (player.thrustersOn)
        {
            player.movementVector -= this.transform.forward * 0.001f;
        }
    }

    public void accelerateRight()
    {
        if (player.thrustersOn)
        {
            player.movementVector += this.transform.right * 0.001f;
        }
    }

    public void accelerateLeft()
    {
        if (player.thrustersOn)
        {
            player.movementVector -= this.transform.right * 0.001f;
        }
    }

    public void accelerateUp()
    {
        if (player.thrustersOn)
        {
            player.movementVector += this.transform.up * 0.001f;
        }
    }

    public void accelerateDown()
    {
        if (player.thrustersOn)
        {
            player.movementVector -= this.transform.up * 0.001f;
        }
    }
    
}
