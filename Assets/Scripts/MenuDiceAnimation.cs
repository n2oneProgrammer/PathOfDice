using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direct
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public class MenuDiceAnimation : MonoBehaviour
{
    public float animTime = 0.5f;
    public Direct[] path;
    public int movementStage;

    void Start()
    {
        StartCoroutine(MoveWithRollCoroutine());
    }

    public void MoveWithRoll()
    {
        StartCoroutine(MoveWithRollCoroutine());
    }

    public IEnumerator MoveWithRollCoroutine()
    {
        var position = transform.position;
        var rotation = transform.rotation;
        var distance = new Vector2(0, 0);
        switch (path[movementStage])
        {
            case Direct.Up:
                distance = new Vector2(-1, 0);
                break;
            case Direct.Down:
                distance = new Vector2(1, 0);
                break;
            case Direct.Left:
                distance = new Vector2(0, -1);
                break;
            case Direct.Right:
                distance = new Vector2(0, 1);
                break;
        }

        Vector3 diffVec = new Vector3(0.5f * distance.x, -0.5f, 0.5f * distance.y);
        Vector3 rotationPoint = position + diffVec;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, diffVec);
        float counter = 0;
        // GameManager.instance.StartMove();
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
        movementStage = (movementStage + 1) % path.Length;
        MoveWithRoll();
    }
}