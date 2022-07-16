using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();


    public Tile GetTile(Vector3 pos)
    {
        var tile = tiles.Find(x => x.transform.position == pos);
        print(tile);
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
}