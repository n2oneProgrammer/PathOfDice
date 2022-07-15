using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance;

    static public GameManager instance
    {
        get { return _instance; }
    }

    public bool isInMove;
    public TileManager tileManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    public void MovedTo(GameObject tile)
    {
        tile.GetComponent<Tile>().OnMove();
    }

    public void StartMove()
    {
        isInMove = true;
    }

    public void EndMove()
    {
        isInMove = false;
    }
}