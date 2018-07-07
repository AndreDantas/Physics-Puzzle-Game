using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class GameSaveSettings
{

}
public class GameSettings : MonoBehaviour
{

    public static GameSettings instance;
    public SettingsPresets currentSettings;
    public SettingsPresets defaultSettings;
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }


}
