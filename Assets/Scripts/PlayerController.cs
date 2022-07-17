using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Dictionary<Vector3, int> faces = new Dictionary<Vector3, int>()
    {
        { Vector3.down, 6 },
        { Vector3.up, 1 },
        { Vector3.back, 5 },
        { Vector3.forward, 2 },
        { Vector3.left, 3 },
        { Vector3.right, 4 }
    };

    [Range(0f, 2f)] public float animTime = 1;
    public ParticleSystem winParticle;
    public AudioSource audioSource;
    public AudioClip slideAudioClip;
    public AudioClip rollAudioClip;
    public AudioClip rotateAudioClip;
    public AudioClip wrongAudioClip;

    float timer = 0;
    bool wasRelised = true;

    private void Start()
    {
        GameManager.instance.onWin.AddListener(OnWin);
    }

    private void Update()
    {
        if (!GameManager.instance.isInMove)
        {
            Vector2 input = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            if (input.x == 0 && input.y == 0)
            {
                wasRelised = true;
            }

            if (wasRelised && timer < 0.15)
            {
                timer += Time.deltaTime;
                return;
            }

            if (input.x > 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(-1, 0), true);
                timer = 0;
                wasRelised = false;
            }
            else if (input.x < 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(1, 0), true);
                timer = 0;
                wasRelised = false;
            }
            else if (input.y > 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(0, 1), true);
                timer = 0;
                wasRelised = false;
            }
            else if (input.y < 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(0, -1), true);
                timer = 0;
                wasRelised = false;
            }
        }
    }

    public bool MoveWithoutRoll(Vector2 newPos, bool playerMove = false)
    {
        var tile = GameManager.instance.tileManager.GetTile(new Vector3(newPos.x, 0, newPos.y));
        if (tile == null || !tile.canPlace())
        {
            audioSource.clip = wrongAudioClip;
            audioSource.Play();
            return false;
        }

        StartCoroutine(MoveWithoutRollCoroutine(newPos, tile, playerMove));
        return true;
    }

    public IEnumerator MoveWithoutRollCoroutine(Vector2 newPos, Tile tile, bool playerMove = false)
    {
        var oldTile = GameManager.instance.tileManager.GetTile(new Vector3((float)Math.Round(transform.position.x), 0,
            (float)Math.Round(transform.position.z)));
        audioSource.clip = slideAudioClip;
        audioSource.Play();

        var position = transform.position;
        float counter = 0;
        GameManager.instance.StartMove(playerMove);
        while (counter < animTime)
        {
            transform.position = Vector3.Lerp(position, new Vector3(newPos.x, 1, newPos.y), counter / animTime);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(newPos.x, 1, newPos.y);
        oldTile.OnRelease();
        GameManager.instance.MovedTo(tile.gameObject);
    }

    public bool MoveWithRoll(Vector2 newPos, bool playerMove = false)
    {
        var tile = GameManager.instance.tileManager.GetTile(new Vector3(newPos.x, 0, newPos.y));
        if (tile == null || !tile.canPlace())
        {
            audioSource.clip = wrongAudioClip;
            audioSource.Play();
            return false;
        }

        StartCoroutine(MoveWithRollCoroutine(newPos, tile, playerMove));
        return true;
    }

    public IEnumerator MoveWithRollCoroutine(Vector2 newPos, Tile tile, bool playerMove = false)
    {
        var oldTile = GameManager.instance.tileManager.GetTile(new Vector3((float)Math.Round(transform.position.x), 0,
            (float)Math.Round(transform.position.z)));
        audioSource.clip = rollAudioClip;
        audioSource.Play();

        var position = transform.position;
        var rotation = transform.rotation;
        var distance = newPos - Utils.Vec3ToVec2(position);
        Vector3 diffVec = new Vector3(0.5f * distance.x, -0.5f, 0.5f * distance.y);
        Vector3 rotationPoint = position + diffVec;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, diffVec);
        float counter = 0;
        GameManager.instance.StartMove(playerMove);
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
        oldTile.OnRelease();
        GameManager.instance.MovedTo(tile.gameObject);
    }

    public bool Rotate(Vector3 angles)
    {
        StartCoroutine(RotateCoroutine(angles));
        return true;
    }

    public IEnumerator RotateCoroutine(Vector3 angles)
    {
        audioSource.clip = rotateAudioClip;
        audioSource.Play();

        var rotation = transform.rotation;
        var targetRotation = Quaternion.Euler(transform.localEulerAngles + angles);

        float counter = 0;
        GameManager.instance.StartMove(false);
        while (counter < animTime)
        {
            transform.rotation = Quaternion.Lerp(rotation, targetRotation, counter / animTime);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        GameManager.instance.EndMove();
    }

    public int getNumber()
    {
        foreach (var item in faces)
        {
            if (transform.rotation * item.Key == Vector3.down)
            {
                return item.Value;
            }
        }

        return -1;
    }

    public void OnWin()
    {
        winParticle.Play();
    }
}