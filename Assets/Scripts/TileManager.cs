using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();


    GameObject GetTile(Vector3 pos)
    {
        return tiles.Find(x => x.transform.position == pos);
    }

    public void AddTile(GameObject tile)
    {
        tiles.Add(tile);
    }
}
