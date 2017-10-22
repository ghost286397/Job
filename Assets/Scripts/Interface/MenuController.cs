using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public SettingsWindow settingsWindow;
    public Translator translator;
    public MMusic music;
    public CanvasGroup interfase = new CanvasGroup();
    public CanvasGroup loadingText = new CanvasGroup();
    public Languages Language = Languages.MULTI;
    public Sounder sounder;
    public Text loadedText;
    public GameObject[] loadedItem;


    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        translator.Init(Language);
        if (!StaticData.loaded)
            StaticData.Init();
        Init();
        sounder.Init();
    }

    void Init()
    {
        // Music
        MMusic.volume = StaticData.Music;
        music.play();
        sounder = GameObject.FindObjectOfType<Sounder>();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        settingsWindow.Init();
        Time.timeScale = 1;

        ParseJson.Parse();
    }

    public void OnPlay()
    {
        loadingText.gameObject.SetActive(true);
        StartCoroutine("LoadGame");
        StartCoroutine("LoadGame2");

    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator LoadGame()
    {
        int pr = 0;
        int item = loadedItem.Length;

        while (loadingText.alpha < 1)
        {
            
            interfase.alpha -= 0.1f;
            loadingText.alpha += 0.1f;
            pr += 10;
            loadedText.text = pr.ToString(); 
            yield return new WaitForFixedUpdate();
        }
        SceneManager.LoadScene("Game");
    }
    private IEnumerator LoadGame2()
    {
        int count_item =  loadedItem.Length;
        int i =0;
        while (count_item < 36)
        {
            loadedItem[i].SetActive(false);
            i++;
            yield return new WaitForFixedUpdate();
        }
    }
}