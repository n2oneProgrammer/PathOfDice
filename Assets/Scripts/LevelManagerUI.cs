using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManagerUI : MonoBehaviour
{
    public GameObject button;
    public LevelsData data;

    void Start()
    {
        for (int i = 0; i < data.levels.Length; i++)
        {
            PleaceButton(i);
        }
    }

    void PleaceButton(int id)
    {
        GameObject obj = Instantiate(button, Vector3.zero, Quaternion.identity, transform);
        Button b = obj.GetComponent<Button>();
        b.onClick.AddListener(()=>LoadLevel(id));
        //b.interactable = PlayerPrefs.GetInt("unlockLevels", 1) > id; // THE CHEAT TODO: remove this

        obj.GetComponentInChildren<TextMeshProUGUI>().text = data.levels[id].name;
    }

    void LoadLevel(int id)
    {
        FindObjectOfType<SceneLoader>().LoadLevel(id);
    }
}
