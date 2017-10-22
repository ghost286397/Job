using UnityEngine;
using System.Collections;

public class Sounder : MonoBehaviour
{

	public int SourcesCount = 5;
	
	AudioSource[] sources;
	
	public void Init ()
	{
		Refresh ();
	}

	[ContextMenu("Refresh")]
	public void Refresh ()
	{
		if (transform.childCount != SourcesCount) {
			for (int i = transform.childCount - 1; i >= 0; i--) {
				DestroyImmediate (transform.GetChild (i).gameObject);
			}

			sources = new AudioSource[SourcesCount];
			
			for (int i = 0; i < SourcesCount; i++) {
				var aSrc = new GameObject ("Source" + (i + 1).ToString ());
				aSrc.transform.parent = transform;
				aSrc.transform.localPosition = Vector3.zero;
				
				sources [i] = aSrc.AddComponent<AudioSource> ();
				sources [i].playOnAwake = false;
				sources [i].loop = false;
			}	
		} else {
			sources = transform.GetComponentsInChildren<AudioSource> ();
		}

	}
	
	public void Play (string name)
	{
		Play (name, StaticData.Sound);
	}

	public void Play (string name, float volume)
	{
		var audio = StaticData.Audio [name];
		foreach (var item in sources) {
			if (!item.isPlaying) {
				item.clip = audio;
				item.volume = volume;
				item.Play ();
				return;
			}
		}
		var sacriface = sources [Random.Range (0, SourcesCount)];
		sacriface.clip = audio;
		sacriface.volume = volume;
		sacriface.Play ();
	}

	public bool IsPlaying (string name)
	{
		var audio = StaticData.Audio [name];

		foreach (var item in sources) 
			if (item.clip == audio) 
				return true;

		return false;
	}

	public void PlayButtonSound ()
	{
		Play ("attack_theme-001");
	}

}
