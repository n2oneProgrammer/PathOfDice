using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 2f)]
    public float animTime = 1;

    public void MoveDown(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        StartCoroutine(MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(1, 0)));
    }

    public void MoveUp(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        StartCoroutine(MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(-1, 0)));
    }

    public void MoveLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        StartCoroutine(MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(0, -1)));
    }

    public void MoveRight(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        StartCoroutine(MoveWithoutRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(0, 1)));
    }

    public IEnumerator MoveWithoutRoll(Vector2 newPos)
    {
        if (GameManager.instance.isInMove) yield break;
        var position = transform.position;

        float counter = 0;
        GameManager.instance.StartMove();
        while (counter < animTime)
        {
            transform.position = Vector3.Lerp(position, new Vector3(newPos.x, 1, newPos.y), counter / animTime);
            counter += Time.deltaTime;
            yield return null;
        }

        GameManager.instance.EndMove();

        transform.position = new Vector3(newPos.x, 1, newPos.y);
    }

    public IEnumerator MoveWithRoll(Vector2 newPos)
    {
        print("as");
        if (GameManager.instance.isInMove) yield break;
        var position = transform.position;
        var rotation = transform.rotation;
        var distance = newPos - Utils.Vec3ToVec2(position);
        Vector3 diffVec = new Vector3(0.5f * distance.x, -0.5f, 0.5f * distance.y);
        Vector3 rotationPoint = position + diffVec;
        print(rotationPoint);
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, diffVec);
        float counter = 0;
        GameManager.instance.StartMove();
        while (counter < animTime)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.RotateAround(rotationPoint, rotationAxis, counter / animTime * 90);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.position = position;
        transform.rotation = rotation;
        transform.RotateAround(rotationPoint, rotationAxis, 90);
        GameManager.instance.EndMove();
    }

    private void Update()
    {
    }
}