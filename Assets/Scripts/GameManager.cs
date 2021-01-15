using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private const string SELECTED_KNIFE = "Knife";
    private const string HIGHSCORE = "Highscore";
    private const string TOTAL_APPLE = "Total Apple";
    private const string SOUND_SETTINGS = "Sound settings";
    private const string VIBRATION_SETTINGS = "Vibration Settings";

    public bool IsGameOver { get; set; }
    public int Stage { get; set; }
    public int Score { get; set; }
    public Knife SelectedKnifePrefab { get; set; }
   



    public float ScreenHeight => Camera.main.orthographicSize * 2;
    public float ScreenWidth => ScreenHeight / Screen.height * Screen.width;

    [Header("For Testing")]
    [SerializeField] int applesAvailable;


    public int SelectedKnife
    {
        get { return PlayerPrefs.GetInt(SELECTED_KNIFE, 0);}
        set { PlayerPrefs.SetInt(SELECTED_KNIFE, value); }
    }

    public int Highscore
    {
        get { return PlayerPrefs.GetInt(HIGHSCORE, 0); }
        set { PlayerPrefs.SetInt(HIGHSCORE, value); }
    }

    public int TotalApple
    {
        get { return PlayerPrefs.GetInt(TOTAL_APPLE, applesAvailable); }
        set { PlayerPrefs.SetInt(TOTAL_APPLE, value); }
    }

    public bool SoundSettings
    {
        get { return PlayerPrefs.GetInt(SOUND_SETTINGS, 1) == 1; }
        set { PlayerPrefs.SetInt(SOUND_SETTINGS, value ?1 :0); }
    }
    public bool VibrationSettings
    {
        get { return PlayerPrefs.GetInt(VIBRATION_SETTINGS, 1) == 1; }
        set { PlayerPrefs.SetInt(VIBRATION_SETTINGS, value ? 1 : 0); }
    }












    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



}









