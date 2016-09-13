using UnityEngine;
using System.Collections;
using System;

public static class MiscHelperFuncs {
    public static float convertToRadians(float degrees)
    {
        return (degrees * Mathf.PI) / 180;
    }

    public static Vector3 convertToVec3(Vector2 inVec)
    {
        return new Vector3(inVec.x, inVec.y, 0.0f);
    }

    public static Vector2 convertToVec2(Vector3 inVec)
    {
        return new Vector2(inVec.x, inVec.y);
    }

    public static float wrapAngle(float angleToWrap)
    {
        if (angleToWrap > Mathf.PI)
        {
            angleToWrap -= Mathf.PI;
            angleToWrap = -Mathf.PI + angleToWrap;
        }

        if (angleToWrap < -Mathf.PI)
        {
            angleToWrap += Mathf.PI;
            angleToWrap = Mathf.Abs(angleToWrap);
            angleToWrap = Mathf.PI - angleToWrap;
        }

        return angleToWrap;
    } 

    public static float convertTo360Angle(float angle)
    {
        angle = angle * (180 / Mathf.PI);
        return angle;
    }

    public static float AngleBetweenVector2(Vector2 source, Vector2 target)
    {
        return (Mathf.DeltaAngle(Mathf.Atan2(source.y, source.x) * Mathf.Rad2Deg,
                                Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg)) * (Mathf.PI/180);
    }
}
