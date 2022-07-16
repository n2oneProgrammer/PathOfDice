using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSave
{
    public String name;
    public int highScore = Int32.MaxValue;
}

public static class LevelSaver
{
    public static LevelSave getSave(String name)
    {
        var saveString = PlayerPrefs.GetString("levelSave_" + name, "NULL");
        if (saveString == "NULL")
        {
            var save = new LevelSave();
            save.name = name;
            return save;
        }
        return JsonUtility.FromJson<LevelSave>(saveString);
    }

    public static void pushSave(LevelSave save)
    {
        PlayerPrefs.SetString("levelSave_" + save.name, JsonUtility.ToJson(save));
    }
}