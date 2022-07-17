using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();

    public Tile GetTile(Vector3 pos)
    {
        var tile = tiles.Find(x => x.transform.position == pos);
        if (tile == null)
        {
            return null;
        }

        return tile;
    }

    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }

    public void removeTile(Tile tile)
    {
        tiles.Remove(tile);
    }
}