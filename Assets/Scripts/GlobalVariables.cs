
public static class GlobalVariables{

    public static float VEL_CONST;
    public static float MOUSE_SENSITIVITY;
    public static float MAX_SPEED;

    //Accessed once at the beginning of the scene on start, should not be accessed ever again. Made to allow editor access to global variables.
    public static void setGlobals(float VEL_CONST, float MOUSE_SENSITIVITY, float MAX_SPEED)
    {
        GlobalVariables.VEL_CONST = VEL_CONST;
        GlobalVariables.MOUSE_SENSITIVITY = MOUSE_SENSITIVITY;
        GlobalVariables.MAX_SPEED = MAX_SPEED;
    }

}
