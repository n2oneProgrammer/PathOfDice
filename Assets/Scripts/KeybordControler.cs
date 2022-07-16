using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class KeybordControler : MonoBehaviour
{
    public GameObject pausePanel;
    public SceneLoadUI sceneLoadUI;

    
    public void RestartKeybord(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        sceneLoadUI.LoadThisLevel();
    }

    public void PauseMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
