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

    private void Start()
    {
        GameManager.instance.onWin.AddListener(OnWin);
    }

    private void Update()
    {
        if (!GameManager.instance.isInMove)
        {
            Vector2 input = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            if (input.x > 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(-1, 0));
            }
            else if (input.x < 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(1, 0));
            }
            else if (input.y > 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(0, 1));
            }
            else if (input.y < 0)
            {
                MoveWithRoll(Utils.Vec3ToVec2(transform.position) + new Vector2(0, -1));
            }
        }
    }

    public void MoveWithoutRoll(Vector2 newPos)
    {
        StartCoroutine(MoveWithoutRollCoroutine(newPos));
    }

    public IEnumerator MoveWithoutRollCoroutine(Vector2 newPos)
    {
        var tile = GameManager.instance.tileManager.GetTile(new Vector3(newPos.x, 0, newPos.y));
        if (tile == null || !tile.canPlace())
        {
            audioSource.clip = wrongAudioClip;
            audioSource.Play();
            yield break;
        };

        audioSource.clip = slideAudioClip;
        audioSource.Play();

        var position = transform.position;
        float counter = 0;
        GameManager.instance.StartMove();
        while (counter < animTime)
        {
            transform.position = Vector3.Lerp(position, new Vector3(newPos.x, 1, newPos.y), counter / animTime);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(newPos.x, 1, newPos.y);
        GameManager.instance.MovedTo(tile.gameObject);
    }

    public void MoveWithRoll(Vector2 newPos)
    {
        StartCoroutine(MoveWithRollCoroutine(newPos));
    }

    public IEnumerator MoveWithRollCoroutine(Vector2 newPos)
    {
        var tile = GameManager.instance.tileManager.GetTile(new Vector3(newPos.x, 0, newPos.y));
        if (tile == null || !tile.canPlace()) {
            audioSource.clip = wrongAudioClip;
            audioSource.Play();
            yield break;
        };

        audioSource.clip = rollAudioClip;
        audioSource.Play();

        var position = transform.position;
        var rotation = transform.rotation;
        var distance = newPos - Utils.Vec3ToVec2(position);
        Vector3 diffVec = new Vector3(0.5f * distance.x, -0.5f, 0.5f * distance.y);
        Vector3 rotationPoint = position + diffVec;
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
        GameManager.instance.MovedTo(tile.gameObject);
    }

    public void Rotate(Vector3 angles)
    {
        StartCoroutine(RotateCoroutine(angles));
    }

    public IEnumerator RotateCoroutine(Vector3 angles)
    {
        var tile = GameManager.instance.tileManager.GetTile(new Vector3((float)Math.Round(transform.position.x), 0,
            (float)Math.Round(transform.position.z)));
        if (tile == null || !tile.canPlace())
        {
            audioSource.clip = wrongAudioClip;
            audioSource.Play();
            yield break;
        };

        audioSource.clip = rotateAudioClip;
        audioSource.Play();

        var rotation = transform.rotation;
        var targetRotation = Quaternion.Euler(transform.localEulerAngles + angles);

        float counter = 0;
        GameManager.instance.StartMove();
        while (counter < animTime)
        {
            transform.rotation = Quaternion.Lerp(rotation, targetRotation, counter / animTime);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        GameManager.instance.EndMove();
    }

    private void OnDrawGizmos()
    {
        var vec = transform.rotation * Vector3.up;
        Debug.DrawRay(transform.position, vec, Color.red);
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