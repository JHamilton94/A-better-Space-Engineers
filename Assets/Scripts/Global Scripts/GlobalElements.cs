using UnityEngine;
using System.Collections;

public class GlobalElements : MonoBehaviour {
    public float VEL_CONST;
    public float MOUSE_SENSITIVITY;
    public float MAX_SPEED;

    //Initializes the Global elements static class, a nessecary exception to the no functions rule for Elements Components
    public void Start()
    {
        GlobalVariables.setGlobals(VEL_CONST, MOUSE_SENSITIVITY, MAX_SPEED);
    }
}
