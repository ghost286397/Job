using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public class ParseJson : MonoBehaviour {

	public static void Parse() {
		Parse1();
		Parse2();
	}
	public static void Parse1() {
		string text = "";
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
						string filePath = System.IO.Path.Combine (Application.dataPath + "/StreamingAssets", "object_map_1.json");
						WWW www = new WWW (filePath);
							while (!www.isDone) {}
							text = www.text;
		#else
						string filePath = System.IO.Path.Combine (Application.streamingAssetsPath, "object_map_1.json");
						if (filePath.Contains ("://")) {
							WWW www = new WWW (filePath);
								while (!www.isDone) {}
							text = www.text;
						}
						else {
		#if !UNITY_METRO || UNITY_EDITOR
							using (System.IO.StreamReader reader = System.IO.File.OpenText (filePath)) {
								text = reader.ReadToEnd ();
							}
		#else
							byte[] byteArray = UnityEngine.Windows.File.ReadAllBytes (filePath);
							char[] charArray = System.Text.UTF8Encoding.UTF8.GetChars (byteArray);
							text = new string (charArray);
		#endif
					}
		#endif
						JSONNode jsonNode = JSON.Parse (text);
						
						JSONArray layers1 = jsonNode ["layers"][0]["data"].AsArray;
						for (int i = 0; i < layers1.Count; i++) {
							StaticData.data_1_1[i]  = layers1[i] ;
							Debug.Log(StaticData.data_1_0[i]);
						}
						JSONArray layers2 = jsonNode ["layers"][1]["data"].AsArray;
						for (int i = 0; i < layers1.Count; i++) {
							StaticData.data_2_1[i]  = layers2[i] ;
						}
						JSONArray layers3 = jsonNode ["layers"][2]["data"].AsArray;
						for (int i = 0; i < layers1.Count; i++) {
							StaticData.data_3_1[i]  = layers3[i] ;
						}
	}

	public static void Parse2() {
		string text = "";
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
						string filePath = System.IO.Path.Combine (Application.dataPath + "/StreamingAssets", "object_map_2.json");
						WWW www = new WWW (filePath);
							while (!www.isDone) {}
							text = www.text;
		#else
						string filePath = System.IO.Path.Combine (Application.streamingAssetsPath, "object_map_2.json");
						if (filePath.Contains ("://")) {
							WWW www = new WWW (filePath);
								while (!www.isDone) {}
							text = www.text;
						}
						else {
		#if !UNITY_METRO || UNITY_EDITOR
							using (System.IO.StreamReader reader = System.IO.File.OpenText (filePath)) {
								text = reader.ReadToEnd ();
							}
		#else
							byte[] byteArray = UnityEngine.Windows.File.ReadAllBytes (filePath);
							char[] charArray = System.Text.UTF8Encoding.UTF8.GetChars (byteArray);
							text = new string (charArray);
		#endif
					}
		#endif
						JSONNode jsonNode = JSON.Parse (text);
						
						JSONArray layers1 = jsonNode ["layers"][0]["data"].AsArray;
						for (int i = 0; i < layers1.Count; i++) {
							StaticData.data_1_0[i]  = layers1[i] ;
							Debug.Log(StaticData.data_1_0[i]);
						}
						JSONArray layers2 = jsonNode ["layers"][1]["data"].AsArray;
						for (int i = 0; i < layers1.Count; i++) {
							StaticData.data_2_0[i]  = layers2[i] ;
						}
						JSONArray layers3 = jsonNode ["layers"][2]["data"].AsArray;
						for (int i = 0; i < layers1.Count; i++) {
							StaticData.data_3_0[i]  = layers3[i] ;
						}
	}
						
}
