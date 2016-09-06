using UnityEngine;
using System.Collections;

public class GlobalElements : MonoBehaviour {
    public float VEL_CONST;
    public float MOUSE_SENSITIVITY;
    public float MAX_SPEED;

    public void Start()
    {
        GlobalVariables.setGlobals(VEL_CONST, MOUSE_SENSITIVITY, MAX_SPEED);
    }
}
