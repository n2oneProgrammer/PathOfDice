using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<TileManager>().AddTile(gameObject);
        GameManager.instance.OnMove.AddListener(OnMoveDone);
    }

    public void OnMoveDone()
    {
        GameManager.instance.EndMove();
    }
}
