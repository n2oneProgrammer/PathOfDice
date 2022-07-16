using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordControler : MonoBehaviour
{
    public GameObject pausePanel;
    public SceneLoadUI sceneLoadUI;

    private void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
        if (Input.GetButtonUp("Restart"))
        {
            sceneLoadUI.LoadThisLevel();
        }
    }

}
