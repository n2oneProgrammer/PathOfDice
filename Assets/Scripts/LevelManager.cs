using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public string name;
    public Sprite image;
    public string sceneName;
}

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int unlockLevels;

    private void Start()
    {
        unlockLevels = PlayerPrefs.GetInt("unlockLevels", 1);
    }

    public void unlockLevel(int levelId)
    {
        unlockLevels = Math.Max(levelId + 2, unlockLevels);
        PlayerPrefs.SetInt("unlockLevels", unlockLevels);
    }

    public string GetLevelSceneName(int levelId)
    {
        if (levels.Length <= levelId) return null;
        if (unlockLevels <= levelId) return null;
        return levels[levelId].sceneName;
    }
}