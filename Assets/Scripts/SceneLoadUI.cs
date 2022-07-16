using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadUI : MonoBehaviour
{
    SceneLoader loader;
    LevelManager levelManager;

    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void LoadScene(string name)
    {
        loader.LoadScene(name);
    }

    public void LoadLevel(int levelId = 0)
    {
        var scene = levelManager.GetLevelSceneName(levelId);
        if (scene == null) return;
        loader.LoadLevel(scene);
    }
}