using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    public string gameScene;
    public string startScene;
    public string menuBackgroundScene;

    public LevelsData data;

    public void LoadScene(string name, bool withBackground = false)
    {
        if (name == null || name == "") return;
        LoadingScreen.SetActive(true);

        StartCoroutine(_LoadScene(name, withBackground));
    }

    public void LoadLevel(int id)
    {
        string name = data.GetLevelSceneName(id);
        if (name == null || name == "") return;
        PlayerPrefs.SetInt("currentLevel", id);
        LoadingScreen.SetActive(true);
        StartCoroutine(_LoadLevel(name));
    }

    IEnumerator _LoadLevel(string name)
    {
        AsyncOperation gameLoading = SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Single);
        AsyncOperation levelLoading = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!gameLoading.isDone || !levelLoading.isDone)
        {
            yield return null;
        }

        EndLoading();
    }

    IEnumerator _LoadScene(string name, bool withBackground = true)
    {
        withBackground = true;
        AsyncOperation loading = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        AsyncOperation background = null;
        if (withBackground)
            background = SceneManager.LoadSceneAsync(menuBackgroundScene, LoadSceneMode.Additive);
        while (!loading.isDone || (withBackground && !background.isDone))
        {
            yield return null;
        }

        EndLoading();
    }


    public void EndLoading()
    {
        LoadingScreen.SetActive(false);
    }

    void Start()
    {
        LoadScene(startScene, true);
    }
}