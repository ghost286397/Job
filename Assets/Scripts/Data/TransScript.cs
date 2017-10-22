using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransScript : MonoBehaviour
{

	Text tm;

	void Start ()
	{
		tm = GetComponent<Text> ();
		if (Translator.Started) 
			tm.text = Translator.Trans (tm.text);
	}

	public string Text {
		get { return tm.text;}
		set { tm.text = Translator.Trans (value);}
	}

}
