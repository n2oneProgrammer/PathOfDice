using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    public string gameScene;
    public string startScene;

    public void LoadScene(string name)
    {
        if (name == null || name == "") return;
        LoadingScreen.SetActive(true);
        StartCoroutine(_LoadScene(name));
    }

    public void LoadLevel(string name)
    {
        if (name == null || name == "") return;
        LoadingScreen.SetActive(true);
        StartCoroutine(_LoadLevel(name));

    }

    IEnumerator _LoadLevel(string name)
    {

        AsyncOperation gameLoading = SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Single);
        AsyncOperation levelLoading = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while(!gameLoading.isDone || !levelLoading.isDone)
        {
            yield return null;
        }

        EndLoading();
    }

    IEnumerator _LoadScene(string name)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        while (!loading.isDone)
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
        LoadScene(startScene);
    }

}
