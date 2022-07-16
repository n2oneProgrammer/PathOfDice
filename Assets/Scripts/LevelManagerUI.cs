using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManagerUI : MonoBehaviour
{
    public LevelsData data;
    public GameObject button;

    void Start()
    {
        foreach (Level level in data.levels)
        {
            PleaceButton(level);
        }
    }

    void PleaceButton(Level level)
    {
        GameObject obj = Instantiate(button, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<Button>().onClick.AddListener(()=>LoadLevel(level.sceneName));
        obj.GetComponentInChildren<TextMeshProUGUI>().text = level.name;
    }

    void LoadLevel(string name)
    {
        FindObjectOfType<SceneLoader>().LoadLevel(name);
    }
}
