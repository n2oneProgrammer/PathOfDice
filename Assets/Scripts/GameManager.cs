using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance;

    static public GameManager instance
    {
        get { return _instance; }
    }

    public bool isInMove;
    public TileManager tileManager;
    public PlayerController player;
    public UnityEvent onWin;

    private void Awake()
    {
        _instance = this;
        if(onWin == null) onWin = new UnityEvent();
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