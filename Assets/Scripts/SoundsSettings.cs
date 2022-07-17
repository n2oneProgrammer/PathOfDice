using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{

    public AudioMixer mixer;
    public string name;
    public GameObject obj;

    private void Start()
    {
        float v = PlayerPrefs.GetFloat(name, 0);
        SetValue(v);
    }

    public void SetValue(float v)
    {
        if(obj != null)
        obj.SetActive(v<0);
        mixer.SetFloat(name, v);
    }

    public void Switch()
    {
        float v = PlayerPrefs.GetFloat(name, 0);
        v = v == 0 ? -80 : 0;
        PlayerPrefs.SetFloat(name, v);
        SetValue(v);
    }
}
