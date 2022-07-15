using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<TileManager>().AddTile(gameObject);
    }

    public void OnMove()
    {
        GameManager.instance.EndMove();
    }

    public bool canPlace()
    {
        return true;
    }
}