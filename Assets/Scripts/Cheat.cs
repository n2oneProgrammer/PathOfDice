using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void Switch()
    {
        PlayerPrefs.SetInt("unlockAll", PlayerPrefs.GetInt("unlockAll", 0) == 0 ? 1 : 0);
    }
}
