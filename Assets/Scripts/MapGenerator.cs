using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2 size;
    public GameObject prefab;
    public GameObject playerPrefab;
    public Transform parent;

    [HideInInspector]
    public GameObject[,] tiles;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        tiles = new GameObject[(int)size.x, (int)size.y];
        for (int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                tiles[x,y] = PlaceTile(new Vector3(x, 0, y));
            }
        }
        print(tiles);
    }

    GameObject PlaceTile(Vector3 pos)
    {
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity, parent);
        return obj;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
