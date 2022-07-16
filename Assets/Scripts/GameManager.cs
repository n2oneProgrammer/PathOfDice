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

    [Header("UI")]
    public GameObject endScreen;
    public float showWinPanelTime;

    private void Awake()
    {
        _instance = this;
        if(onWin == null) onWin = new UnityEvent();
        onWin.AddListener(OnWin);
    }

    private void Start()
    {
        endScreen.SetActive(false);
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


    void OnWin()
    {
        int i = Math.Max((PlayerPrefs.GetInt("currentLevel", 0) + 2) ,PlayerPrefs.GetInt("unlockLevels", 1));
        PlayerPrefs.SetInt("unlockLevels", i);
        StartCoroutine(OnWinCorutine());
    }

    IEnumerator OnWinCorutine()
    {
        yield return new WaitForSecondsRealtime(showWinPanelTime);
        endScreen.SetActive(true);
    }
}