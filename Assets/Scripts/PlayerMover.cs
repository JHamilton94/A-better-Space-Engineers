using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    private PlayerElements player;
    

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerElements>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movePlayer();
	}

    private void movePlayer()
    {
        //Rotate Camera
        this.transform.position += player.movementVector;
    }

    public void accelerateForward()
    {
        player.movementVector += this.transform.forward * 0.001f;
    }

    public void accelerateBackwards()
    {
        player.movementVector -= this.transform.forward * 0.001f;
    }

    public void accelerateRight()
    {
        player.movementVector += this.transform.right * 0.001f;
    }

    public void accelerateLeft()
    {
        player.movementVector -= this.transform.right * 0.001f;
    }

    public void accelerateUp()
    {
        player.movementVector += this.transform.up * 0.001f;
    }

    public void accelerateDown()
    {
        player.movementVector -= this.transform.up * 0.001f;
    }


}
