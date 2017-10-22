using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioSettingsItem : MonoBehaviour
{
	Button button;

	float value;
	public AudioType audioType = AudioType.Music;

    public Sprite ButtonOn, ButtonOff;

	public float Value {
		get {
			return value;
		}
		set {
			this.value = value;
			if (value == 0)
                button.GetComponent<Image>().sprite = ButtonOff;
			else
                button.GetComponent<Image>().sprite = ButtonOn;
		}
	}

	public void Init ()
	{
		button = transform.Find("Button").GetComponent<Button> ();
        // slider = transform.Find("Slider").GetComponent<Slider>();

		if (audioType == AudioType.Music || audioType == AudioType.All) {
			Value = StaticData.Music;
			MMusic.volume = value;
		} 
		if (audioType == AudioType.Sound || audioType == AudioType.All) {
			Value = StaticData.Sound;
		} 
		// slider.value = Value;
	}

	public void SetValue (float val)
	{
		Value = val;
		if (audioType == AudioType.Music || audioType == AudioType.All) {
			StaticData.Music = value;
			MMusic.volume = value;
		} 
		if (audioType == AudioType.Sound || audioType == AudioType.All) {
			StaticData.Sound = value;
		} 
	}


	public void ButtonClicked ()
	{
		if (Value == 0) {
			SetValue (1);
		} else {
			SetValue (0);
		}
	}

	public enum AudioType
	{
		Sound,
		Music,
		All
	}
}
