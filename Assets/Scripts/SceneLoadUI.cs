using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadUI : MonoBehaviour
{
    public LevelsData data;
    SceneLoader loader;
    public string levelSelectScene;
    public string endScene;

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
        if (PlayerPrefs.GetInt("unlockLevels", 1) >= data.levels.Length)
        {
            loader.LoadScene(levelSelectScene);
            return;
        }

        loader.LoadLevel(PlayerPrefs.GetInt("unlockLevels", 1) - 1);
    }

    public void LoadLevel(int id)
    {
        loader.LoadLevel(id);
    }

    public void LoadNextLevel()
    {
        if (PlayerPrefs.GetInt("currentLevel", 1) + 1 >= data.levels.Length)
        {
            loader.LoadScene(endScene);
            return;
        }

        loader.LoadLevel(PlayerPrefs.GetInt("currentLevel", 0) + 1);
    }

    public void LoadThisLevel()
    {
        loader.LoadLevel(PlayerPrefs.GetInt("currentLevel", 0));
    }
}