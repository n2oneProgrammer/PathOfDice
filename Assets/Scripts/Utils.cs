using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
   public static Vector2 Vec3ToVec2(Vector3 vec)
   {
      return new Vector2(vec.x, vec.z);
   }
}
