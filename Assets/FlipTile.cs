using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTile : Tile
{
    public override void OnMove()
    {
        Vector3 pos = transform.position + transform.rotation * Vector3.forward;
        GameManager.instance.player.MoveWithRoll(Utils.Vec3ToVec2(pos));
    }
}
