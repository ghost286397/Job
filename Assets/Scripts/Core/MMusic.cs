using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MMusic : MonoBehaviour
{
		public static Hashtable musics;

		static float _volume = 1;
		public static float volume {
				get {
						return _volume;
				}
				set {
						_volume = value;
						if (musics != null) {
								foreach (MMusic music in musics.Values) {
										music.GetComponent<AudioSource>().volume = _volume;
								}
						}
				}
		}

		void Awake ()
		{
				if (musics == null) {
						musics = new Hashtable ();
				}

				GetComponent<AudioSource>().volume = _volume;

				if (!musics.ContainsKey (name)) {
						musics.Add (name, this);
						DontDestroyOnLoad (this);
				} else {
						GetComponent<AudioSource>().Stop ();
						Destroy (gameObject);
				}
		}

		public void play ()
		{
				GetComponent<AudioSource>().Play ();
		}
	
		public static void stop (string name)
		{
				foreach (MMusic music in musics) {
						if (music.gameObject.name == name) {
								music.GetComponent<AudioSource>().Stop ();
								return;
						}
				}
		}
	
		public static void stopAll ()
		{
				foreach (MMusic music in musics.Values) {
						music.GetComponent<AudioSource>().Stop ();
				}
		}
}
