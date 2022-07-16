using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadUI : MonoBehaviour
{
    public LevelsData data;
    SceneLoader loader;

    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    public void LoadScene(string name)
    {
        loader.LoadScene(name);
    }

    public void LoadLastLevel()
    {
        loader.LoadLevel(PlayerPrefs.GetInt("unlockLevels", 1) - 1);
    }

    public void LoadLevel(int id)
    {
        loader.LoadLevel(id);
    }

    public void LoadNextLevel()
    {
        loader.LoadLevel(PlayerPrefs.GetInt("currentLevel", 0) + 1);
    }

    public void LoadThisLevel()
    {
        loader.LoadLevel(PlayerPrefs.GetInt("currentLevel", 0));
    }
}