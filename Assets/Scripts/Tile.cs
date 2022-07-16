using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        None = 0,
        Flip = 1,
        Move = 2,
        Rotate = 3,
        End = 4,
    }

    public enum RotateDirection
    {
        Left = -90,
        Right = 90,
    }

    public enum EndType
    {
        All = 0,
        n1 = 1,
        n2 = 2,
        n3 = 3,
        n4 = 4,
        n5 = 5,
        n6 = 6,
    }

    public TileType type;
    public RotateDirection direction = RotateDirection.Left;
    public EndType endType;

    private void Start()
    {
        FindObjectOfType<TileManager>().AddTile(this);
    }

    public void OnMove()
    {
        switch (type)
        {
            case TileType.Flip:
                {
                    Vector3 pos = transform.position + transform.rotation * Vector3.forward;
                    GameManager.instance.player.MoveWithRoll(Utils.Vec3ToVec2(pos));
                    break;
                }
            case TileType.Move:
                {
                    Vector3 pos = transform.position + transform.rotation * Vector3.forward;
                    GameManager.instance.player.MoveWithoutRoll(Utils.Vec3ToVec2(pos));
                    break;
                }
            case TileType.Rotate:
                {
                    GameManager.instance.player.Rotate(new Vector3(0, (float)direction, 0));
                    break;
                }
            case TileType.End:
                {
                    if (endType == EndType.All || GameManager.instance.player.getTopNumber() == (int)endType)
                    {
                        GameManager.instance.onWin.Invoke();
                    }
                    else
                    {
                        GameManager.instance.EndMove();
                    }

                    break;
                }
            default:
                GameManager.instance.EndMove();
                break;
        }
        
    }

    public bool canPlace()
    {
        return true;
    }
}