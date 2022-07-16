using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager instance => _instance;

    public bool isInMove;
    public TileManager tileManager;
    public PlayerController player;
    public UnityEvent onWin;
    public LevelsData LevelsData;

    public AudioSource audioSource;
    public AudioClip winAudioClip;
    [Header("UI")] public GameObject endScreen;
    public float showWinPanelTime;
    public TMPro.TextMeshProUGUI scoreText;

    private int moveCount = 0;
    int lastScore = 0;

    private void Awake()
    {
        _instance = this;
        if (onWin == null) onWin = new UnityEvent();
        onWin.AddListener(OnWin);
    }

    private void Start()
    {
        endScreen.SetActive(false);
        tileManager = FindObjectOfType<TileManager>();
        string levelName = LevelsData.GetLevelSceneName(PlayerPrefs.GetInt("currentLevel", 0));
        lastScore = LevelSaver.getSave(levelName).highScore;
        moveCount = 0;
        DisplayScore();
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
        moveCount++;
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = moveCount + "/" + (lastScore == Int32.MaxValue ? "-" : lastScore);
    }

    void OnWin()
    {
        audioSource.clip = winAudioClip;
        audioSource.Play();
        moveCount++;
        DisplayScore();
        int i = Math.Max((PlayerPrefs.GetInt("currentLevel", 0) + 2), PlayerPrefs.GetInt("unlockLevels", 1));
        PlayerPrefs.SetInt("unlockLevels", i);
        StartCoroutine(OnWinCorutine());
        string levelName = LevelsData.GetLevelSceneName(PlayerPrefs.GetInt("currentLevel", 0));
        var save = LevelSaver.getSave(levelName);
        if (save.highScore > moveCount)
        {
            save.highScore = moveCount;
            LevelSaver.pushSave(save);
        }
    }

    IEnumerator OnWinCorutine()
    {
        yield return new WaitForSecondsRealtime(showWinPanelTime);
        endScreen.SetActive(true);
    }
}