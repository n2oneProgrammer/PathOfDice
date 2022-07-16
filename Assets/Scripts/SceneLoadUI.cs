using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadUI : MonoBehaviour
{
    SceneLoader loader;

    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    public void LoadScene(string name)
    {
        loader.LoadScene(name);
    }

    public void LoadLevel(string name)
    {
        if (name == null) return;
        loader.LoadLevel(name);
    }
}