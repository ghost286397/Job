using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsWindow : Window
{

    public AudioSettingsItem sound, music;

    public override void Init()
    {
        sound.Init();
        music.Init();
        // base.Init();
    }

    public override void Hide()
    {
        StaticData.SaveSettings();
        base.Hide();
    }
}
