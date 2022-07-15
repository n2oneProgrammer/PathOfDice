using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance;
    static public GameManager instance { get { return _instance; } }

    public bool isInMove;

    private void Awake()
    {
        _instance = this;
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

    void Start()
    {

    }

    void Update()
    {
        
    }
}
