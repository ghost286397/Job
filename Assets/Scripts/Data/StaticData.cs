using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class StaticData
{
    public static float speed_hero = 3.5f;
	public static float Sound = 1f, Music = 1f;
    public static bool game_active = true;
    public static bool run_active = true;
    public static int Level = 1;

	public static int[] data_1_0 = new int[120];
	public static int[] data_2_0 = new int[120];
	public static int[] data_3_0 = new int[120];

	public static int[] data_1_1 = new int[120];
	public static int[] data_2_1 = new int[120];
	public static int[] data_3_1 = new int[120];

    public static bool TutorialEnabled = true;
    public static int TutorialStep = 0;

	public static void Init () {
        //Clear();
		LoadSprites ();
		LoadAudio ();
		LoadSettings ();
		Load ();
	    InitData();
		loaded = true;
	}

	public static GameObject LoadPrefab (string name) {
		return Resources.Load ("Prefab/" + name) as GameObject;
	}

    public static Dictionary<string, Sprite> TileSprites;
    public static Dictionary<string, Sprite> TileSpritesOb;
    
	static void LoadSprites () {
        TileSprites = new Dictionary<string, Sprite>();
        Sprite[] tmpSpriteMass = Resources.LoadAll<Sprite>("maps");

        for (int i = 0; i < 210; i++) {
        int a = i + 1;
        string id_name = "id_" + a;  
        string id_tile = "grounds_reformat_" + i;  
        TileSprites.Add(id_name, tmpSpriteMass.First(x => x.name == id_tile));
        }

        TileSpritesOb = new Dictionary<string, Sprite>();
        for (int i = 0; i < 48; i++) {
        int a = i + 211;
        string id_name2 = "id_" + a;  
        string id_tile2 = "game_items_reformat_" + i;  
        TileSpritesOb.Add(id_name2, tmpSpriteMass.First(x => x.name == id_tile2));
        }

	}

	public static Dictionary<string, AudioClip> Audio;
	
	static void LoadAudio () {
		string path = "Sound/";
		Audio = new Dictionary<string, AudioClip> ();

		Audio.Add ("attack_theme-001", Resources.Load<AudioClip> (path + "attack_theme-001"));
	
    }



	#region SaveLoad
	public static bool loaded;
	public static void Save () {
        PlayerPrefs.SetInt("Level",Level);
	}
	public static void SaveSettings () {
		PlayerPrefs.SetFloat ("Sound", Sound);
		PlayerPrefs.SetFloat ("Music", Music);
	}
	public static void Load () {
        Level = PlayerPrefs.GetInt("Level", 1);
	}
	public static void LoadSettings () {
		if (PlayerPrefs.HasKey ("Sound"))
			Sound = PlayerPrefs.GetFloat ("Sound");
		if (PlayerPrefs.HasKey ("Music"))
			Music = PlayerPrefs.GetFloat ("Music");
	}

	public static void Clear () {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}
	#endregion

    private static void InitData()
    {
        
    }

}





