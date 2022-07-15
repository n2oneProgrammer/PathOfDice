using UnityEngine;
using UnityEngine.InputSystem;

public enum Direct
{
    Up = 0,
    Down= 1,
    Left = 2,
    Right = 3
}
public class PlayerController : MonoBehaviour
{
    public float moveTimeout = 2;
    public void MoveDown(InputAction.CallbackContext ctx)
    {
        if(ctx.phase != InputActionPhase.Performed) return;
        MoveWithoutRoll(Utils.Vec3ToVec2(transform.position)+new Vector2(1,0));
    }
    public void MoveUp(InputAction.CallbackContext ctx)
    {
        if(ctx.phase != InputActionPhase.Performed) return;
        MoveWithoutRoll(Utils.Vec3ToVec2(transform.position)+new Vector2(-1,0));
    }
    public void MoveLeft(InputAction.CallbackContext ctx)
    {
        if(ctx.phase != InputActionPhase.Performed) return;
        MoveWithoutRoll(Utils.Vec3ToVec2(transform.position)+new Vector2(0,-1));
    }
    public void MoveRight(InputAction.CallbackContext ctx)
    {
        if(ctx.phase != InputActionPhase.Performed) return;
        MoveWithoutRoll(Utils.Vec3ToVec2(transform.position)+new Vector2(0,1));
    }

    public void MoveWithoutRoll(Vector2 newPos)
    {
        transform.position = new Vector3(newPos.x, 1, newPos.y);
    }
}