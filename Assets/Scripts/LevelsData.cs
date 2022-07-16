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

[CreateAssetMenu()]
public class LevelsData: ScriptableObject
{
    public Level[] levels;

    public string GetLevelSceneName(int levelId)
    {
        if (levels.Length <= levelId) return null;
        if (PlayerPrefs.GetInt("unlockLevels", 1) <= levelId && PlayerPrefs.GetInt("unlockAll", 0) == 0) return null;
        return levels[levelId].sceneName;
    }
}