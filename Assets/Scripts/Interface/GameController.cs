using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    
    public static event System.Action<int> PositionHero;
    public Text score_text;
    public Text score_txt;
    public Text highscore_txt;
    public Translator translator;
    public MMusic music;
    public SettingsWindow settingsWindow;
    public CanvasGroup MenuPanel = new CanvasGroup();
    public CanvasGroup ScorePanel = new CanvasGroup();
    public Languages Language = Languages.MULTI;
    public Sounder sounder;
    public RectTransform[]  maps;
    public Canvas Canvas_game;
    public GameObject[] Item;
    public GameObject[] Item2;
    public GameObject[] ItemTest;
    public GameObject ScorePanelOb;
    public RectTransform hero;
    public RectTransform canvas_map;
    public RectTransform camera;
    private static float length_map_x;
    private static float length_map_y;
    bool chek = false;
    private static int k , n_map;
    float start_hero;
    int score;
    public RectTransform pla;
    private int position_hero;
    float width_map;
    float step = 210;
    int[,] array = new int[20,6];
    int[,] array2 = new int[20,6];

    List<string> listGenOb = new List<string> {"boxes", "mine", "turret", "choper", "bomber"};

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        translator.Init(Language);
        if (!StaticData.loaded)
            StaticData.Init();
        Init();
        sounder.Init();
        StaticData.game_active=true;
    }

    void Init()
    {
        // Music
        MMusic.volume = StaticData.Music;
        music.play();
        sounder = GameObject.FindObjectOfType<Sounder>();

        // settingsWindow.Init();
        Time.timeScale = 1;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause() {
        StaticData.game_active = false;
    }
    public void Resume() {
        StartCoroutine("ScoreCk");
        StaticData.game_active = true;
    }

    public void Play()
    {
        pla.transform.position = new Vector3(0,0,0);
        StaticData.game_active = true;
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ScorePanelH(bool active) {
        if (active){
            ScoreHandler();
            ScorePanel.alpha = 1f;
            ScorePanel.blocksRaycasts = true;

        }
        else {
            ScorePanel.alpha = 0f;
            ScorePanel.blocksRaycasts = false;
        }
    }

    void Start() {
        StaticData.run_active = true;
        width_map = canvas_map.rect.width;
        position_hero = 4;
        float start_pos = step/2;
        hero.transform.localPosition = new Vector3(start_pos, hero.transform.localPosition.y, hero.transform.localPosition.z);

        start_hero = hero.transform.position.y;
        k = 1;
        n_map = 0;
        length_map_y = maps[0].transform.localPosition.y;
        length_map_x = maps[0].localPosition.x;
        SettinMap(0);
        StartCoroutine("ScoreCk");
        CreateMap(Item, 0);
        CreateMap(Item2, 1);
    }

    void SettinMap(int n) {
        n_map = (n == 0) ? 1 : 0;  
        maps[n_map].localPosition = new Vector2(0, 4200 * 0.45f * k);
        k ++;
    }

    void ClearMap(){
        GameObject[] map1;
        map1 = (n_map == 0) ? Item : Item2; 
        for (int i = 0; i < 120; i++) {
            if(map1[i].gameObject.transform.childCount == 2){
                GameObject delete = map1[i].transform.GetChild(1).gameObject;
                Destroy(delete);
            }
            GameObject ttest = map1[i].transform.GetChild(0).gameObject;
            ttest.SetActive(true);
        }
        int random = Random.Range(0,1);
        CreateMap(map1, random);
    }


    void Update() {
          if (Input.GetKey(KeyCode.F)) {
           Score(1100);
        }
        if (!StaticData.game_active) {
            StopCoroutine("ScoreCk");
        }

        score_text.text = score.ToString();

        if (StaticData.game_active) {
            var tt = pla.transform.position;
            tt.y += StaticData.speed_hero;
            pla.transform.position = tt;
            camera.transform.position = new Vector3(camera.transform.position.x, hero.transform.position.y + 200, camera.transform.position.z);
		}
    }

    void FixedUpdate() {
        if (hero.transform.position.y > maps[n_map].transform.position.y - 200) {
           chek = true;
           if (chek) {
               chek = false;
               SettinMap(n_map);
               ClearMap();
           }
        }
    }

    void Score(int sco) {
        score = score + sco;
    }

    private IEnumerator ScoreCk()
    {
        while (score < 100000)
        {
            float i = start_hero - hero.transform.position.y;
            int sco = (int)(-i/100);
            Score(sco);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CreateMap(GameObject[] map, int rand)
    {
        int count_item = map.Length;
        int[] data1;
        int[] data2;
        int[] data3;  

        int[,] massiv; 

       if (rand == 1) {
           data1 = StaticData.data_1_0;
           data2 = StaticData.data_2_0;
           data3 = StaticData.data_3_0;
           massiv = array2;
       }
       else {
           data1 = StaticData.data_1_1;
           data2 = StaticData.data_2_1;
           data3 = StaticData.data_3_1;
           massiv = array;
       }

        int ii = 0;
        int jj = -1;

        for (int i = 0; i < 120; i++) {
            int id1 = data1[i];
            int id2 = data2[i];
            int id3 = data3[i];
            int c = 0;
            map[i].GetComponent<Image>().sprite = StaticData.TileSprites["id_" + id1];
            if (id2 != 0) {
                c = 1;
                map[i].gameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = StaticData.TileSpritesOb["id_" + id2];
            }
            else if (id3 != 0) {
                c = 1;
                map[i].gameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = StaticData.TileSpritesOb["id_" + id3];
            }
            else {
                GameObject ttest = map[i].transform.GetChild(0).gameObject;
                ttest.SetActive(false);
            }
            
            if ( (i)%6 == 0 ) {
				ii = 0;
				jj +=1;
			}
            massiv[jj, ii] = c ;
            ii++;
		}
        for (int i = 0; i<3; i++){
            int ran = Random.Range(0, 3);
            Gen(map, massiv, listGenOb[ran] );        
        }
        
    }


    void Gen(GameObject[] this_map, int[,] massiv, string name_gen) {
        int randX = Random.Range(1, 4);
        int randY = Random.Range(1, 18);
        if (CheckPos(randY, randX, massiv)) {
            massiv[randY, randX] = 2 ;
            GameObject itemTile2 = Instantiate(Resources.Load<GameObject>(name_gen)) as GameObject;
			itemTile2.name = name_gen;	
            int nom = randY * 6 + randX;
			itemTile2.transform.SetParent(this_map[nom].transform);
			itemTile2.transform.localScale = new Vector3 (1,1,1);
			RectTransform rt2 = itemTile2.GetComponent<RectTransform>();
   			rt2.sizeDelta = new Vector2(210, 210);
			itemTile2.transform.localPosition = new Vector3 (0, 0, 0);
			BoxCollider2D bc = (BoxCollider2D)itemTile2.AddComponent(typeof(BoxCollider2D));
     		bc.size = new Vector2 (190, 190);
            if (name_gen != "mine"){
                bc.isTrigger = true;
            }
            if (name_gen == "turret"){
              Turret sc = itemTile2.GetComponent<Turret>();
              sc.PositionTurret = randX+1;
            }
			Rigidbody2D rb = (Rigidbody2D)itemTile2.AddComponent(typeof(Rigidbody2D));
    		rb.gravityScale = 0;
			rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
        }
        else {
            Gen(this_map, massiv, name_gen);
        }
    }

    bool CheckPos(int randY, int randX, int[,] massiv) {
        if (massiv[randY, randX] == 0) {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (massiv[randY + j, randX + i] == 0) {
                    }
                    else {
                        return false;
                    }
                }
            }
        return true;
        }
        else {
            return false;
        }
    }

    void ScoreHandler () {
        string key = "score";
        score_txt.text = score.ToString();

        if(PlayerPrefs.HasKey(key)) {
           int sco = PlayerPrefs.GetInt(key);
           if (score > sco) {
               highscore_txt.text = score.ToString();
               PlayerPrefs.SetInt (key, score);
		       PlayerPrefs.Save (); 
           }
           else {
               highscore_txt.text = sco.ToString();
           }
        }
        else {
             highscore_txt.text = score.ToString();
        PlayerPrefs.SetInt (key, score);
		PlayerPrefs.Save (); 
        }
    }

    public void GoLeft() {
        Debug.Log(position_hero);
        Debug.Log(StaticData.run_active);
        if (position_hero != 2 && StaticData.run_active) {
            Debug.Log("GoLeft");
            position_hero -=1;
            StartCoroutine("GoHeroL");
        }
    }
    public void GoRight() {
        Debug.Log(position_hero);
        if (position_hero != 5 && StaticData.run_active){
            Debug.Log("GoRight");
            position_hero +=1;
            StartCoroutine("GoHeroR");
        }
    }


    private IEnumerator GoHeroL()
    {
        if (StaticData.run_active == true){
        StaticData.run_active = false;
        float hero_x = hero.transform.localPosition.x;
        var tt = hero.transform.localPosition;
        while (hero.transform.localPosition.x >= hero_x - step)
        {
            tt.x -= 20f;
            hero.transform.localPosition = tt;
            yield return new WaitForFixedUpdate();
        }
        hero.transform.localPosition = new Vector3(hero_x - step, hero.transform.localPosition.y, hero.transform.localPosition.z);
        
        StaticData.run_active = true;
        PositionHero(position_hero);
        }
    }

    private IEnumerator GoHeroR()
    {
        if (StaticData.run_active == true){
        StaticData.run_active = false;
        float hero_x = hero.transform.localPosition.x;
        var tt = hero.transform.localPosition;  
         while (hero.transform.localPosition.x <= hero_x + step)
        {
            tt.x += 20f;
            hero.transform.localPosition = tt;
            yield return new WaitForFixedUpdate();
        }
        hero.transform.localPosition = new Vector3(hero_x + step, hero.transform.localPosition.y, hero.transform.localPosition.z);
        
        StaticData.run_active = true;
        PositionHero(position_hero);
        }
    }

}