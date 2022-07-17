using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2 Vec3ToVec2(Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }

    public static Quaternion NormalizeRotation(Quaternion quaternion)
    {
        Vector3 rotation = quaternion.eulerAngles;
        rotation = new Vector3((float)Math.Round(rotation.x), (float)Math.Round(rotation.y),
            (float)Math.Round(rotation.z));
        return Quaternion.Euler(rotation);
    }
}