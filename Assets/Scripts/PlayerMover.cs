using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    private PlayerElements player;
    

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movePlayer();
	}

    private void movePlayer()
    {
        //Rotate Camera
        

        //assemble movement vector
        player.movementVector = new Vector3(player.horizontalAxismagnitude * GlobalVariables.VEL_CONST, player.forwardAxismagnitude * GlobalVariables.VEL_CONST, player.verticalAxisMagnitude * GlobalVariables.VEL_CONST);
        
    }

    public void accelerateForward()
    {
        if (player.forwardAxismagnitude < 1)
        {
            player.forwardAxismagnitude += 0.1f;
        }
    }

    public void accelerateBackwards()
    {
        if (player.forwardAxismagnitude > -1)
        {
            player.forwardAxismagnitude -= 0.01f;
        }
    }

    public void accelerateRight()
    {
        if (player.horizontalAxismagnitude < 1)
        {
            player.horizontalAxismagnitude += 0.1f;
        }
    }

    public void accelerateLeft()
    {
        if (player.horizontalAxismagnitude > -1)
        {
            player.horizontalAxismagnitude -= 0.01f;
        }
    }


}
