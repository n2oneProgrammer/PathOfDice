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

        if (PlayerPrefs.GetInt("unlockLevels", 1) <= id && PlayerPrefs.GetInt("unlockAll",0) == 0)
        {
            obj.transform.Find("Panel").gameObject.SetActive(true);
            b.interactable = false;
        }

        obj.GetComponentInChildren<TextMeshProUGUI>().text = data.levels[id].name;
        obj.GetComponentsInChildren<Image>()[1].sprite = data.levels[id].image;
        var save = LevelSaver.getSave(data.levels[id].sceneName);
        obj.transform.Find("Best").gameObject.GetComponent<TextMeshProUGUI>().text = "" + (save.highScore == System.Int32.MaxValue ? "-" : save.highScore);

    }

    void LoadLevel(int id)
    {
        FindObjectOfType<SceneLoader>().LoadLevel(id);
    }
}
