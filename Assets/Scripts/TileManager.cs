using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();


    public Tile GetTile(Vector3 pos)
    {
        var tile = tiles.Find(x => x.transform.position == pos);
        if (tile == null)
        {
            return null;
        }

        return tile.GetComponent<Tile>();
    }

    public void AddTile(GameObject tile)
    {
        tiles.Add(tile);
    }
}