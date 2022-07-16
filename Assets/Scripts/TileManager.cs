using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Serializable]
    public class BoardDetails
    {
        public float rotationAngle = 60;
        public float distance = 10;
        public float height = 10;
        public Transform startCorner, endCorner;
        private int minX, maxX;
        private int minY, maxY;
        private int centerX, centerY;

        public BoardDetails()
        {
            maxX = Int32.MinValue;
            minX = Int32.MaxValue;
            maxY = Int32.MinValue;
            minY = Int32.MaxValue;
            centerX = 0;
            centerY = 0;
        }

        public void calcCenter(Vector3 position)
        {
            // minX = Math.Min(minX, (int)position.x);
            // minY = Math.Min(minY, (int)position.z);
            // maxX = Math.Max(minX, (int)position.x);
            // maxY = Math.Max(minY, (int)position.z);
            // centerX = (minX + maxX) / 2;
            // centerY = (minY + maxY) / 2;
            // Camera.main.transform.position = new Vector3(
            //     (float)(Math.Sin(rotationAngle * Math.PI / 180) * distance),
            //     height,
            //     (float)(Math.Cos(rotationAngle * Math.PI / 180) * distance)
            // );
            // Camera.main.transform.LookAt(new Vector3(centerX, 0, centerY));
            // startCorner.position = new Vector3(minX, 0,minY);
            // endCorner.position = new Vector3(maxX, 0,maxY);
        }
    }

    List<Tile> tiles = new List<Tile>();

    public BoardDetails details;
    
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
        details.calcCenter(tile.gameObject.transform.position);
        tiles.Add(tile);
    }
}