
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    public Wheel [] wheels;
    public Boss [] bosses;

    [SerializeField] GameObject knifePrefab;


    [Header("Wheel Settings")]
    [SerializeField] Transform wheelSpawnPosition;
    [Range(0, 1)] [SerializeField] float wheelScale;



    [Header("Knife Settings")]
    [SerializeField] Transform knifeSpawnPosition;
    [Range(0, 1)] [SerializeField] float knifeScale;



    private string bossName;
    private Wheel currentWheel;
    private Knife currentKnife;

    public string BossName { get => bossName; }

    public int TotalSpawnKnives { get; set; }



    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
         //   DontDestroyOnLoad(gameObject); because it references other game objects and they will be destroyed anyway
        }

      
    }


    private void Start()
    {
        InitializedGame();
    }

    private void Update()
    {
        if (currentKnife == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !currentKnife.isReleased)
        {
            KnifeCounter.Instance.KnifeHit(TotalSpawnKnives);
            currentKnife.FireKnife();
            StartCoroutine(GenerateKnife());
        }
    }

    private void InitializedGame()
    {
        GameManager.Instance.IsGameOver = false;
        GameManager.Instance.Score = 0; 
        GameManager.Instance.Stage = 1;

        SetUpGame();
    }

    private void SetUpGame()
    {



        SpawnWheel();
       

        KnifeCounter.Instance.SetUpKnife(currentWheel.AvailableKnives);

        TotalSpawnKnives = 0;

        StartCoroutine(GenerateKnife());
    }

    private void SpawnWheel()
    {
        GameObject temporaryWheel = new GameObject();


        if (GameManager.Instance.Stage % 5 == 0)
        {
            //Create the boss wheel
            Boss newBoss = bosses[UnityEngine.Random.Range(0, bosses.Length)];

            temporaryWheel = Instantiate(newBoss.bossPrefab, wheelSpawnPosition.position, Quaternion.identity, wheelSpawnPosition).gameObject;


            bossName = "Boss " + newBoss.bossName;
        }
        else
        {
            temporaryWheel = Instantiate(wheels[GameManager.Instance.Stage - 1], wheelSpawnPosition.position, Quaternion.identity, wheelSpawnPosition).gameObject;
        } 

            float wheelScaleOnTheScreen = GameManager.Instance.ScreenWidth * wheelScale / temporaryWheel.GetComponent<SpriteRenderer>().bounds.size.x;
            temporaryWheel.transform.localScale = Vector3.one * wheelScaleOnTheScreen;
            currentWheel = temporaryWheel.GetComponent<Wheel>();
    }

    
    

    private IEnumerator GenerateKnife()
    {
        yield return new WaitUntil(() => knifeSpawnPosition.childCount == 0); // makes it work until knives are not 0

        if (currentWheel.AvailableKnives > TotalSpawnKnives && !GameManager.Instance.IsGameOver)
        {
            TotalSpawnKnives++;
            GameObject temporaryKnife = new GameObject();

            if (GameManager.Instance.SelectedKnifePrefab == null)
            {
                temporaryKnife = Instantiate(knifePrefab, knifeSpawnPosition.position, Quaternion.identity, knifeSpawnPosition).gameObject;
            } else
            {
                temporaryKnife = Instantiate(GameManager.Instance.SelectedKnifePrefab, knifeSpawnPosition.position, Quaternion.identity, knifeSpawnPosition).gameObject;
            }
            float knifeScaleOnTheScreen = GameManager.Instance.ScreenHeight * knifeScale / temporaryKnife.GetComponent<SpriteRenderer>().bounds.size.y;
            temporaryKnife.transform.localScale = Vector3.one * knifeScaleOnTheScreen;
            currentKnife = temporaryKnife.GetComponent<Knife>();
        }
    }


    public void NextLevel()
    {
        if (currentWheel != null)
        {
            currentWheel.DestroyKnife();
        }
        if (GameManager.Instance.Stage %5 == 0)
        {
            GameManager.Instance.Stage++;
            StartCoroutine(BossDefeated());
        } else
        {
            GameManager.Instance.Stage++;
         

            if (GameManager.Instance.Stage %5 == 0)
            {
                StartCoroutine(BossFight());
            }
            else
            {
                Invoke(nameof(SetUpGame), .3f);
            }
        }
    }
    private IEnumerator BossFight()
    {
        StartCoroutine(UIManager.Instance.BossStart());
        yield return new WaitForSeconds(2f);
        SetUpGame();
    }

    private IEnumerator BossDefeated()
    {
        StartCoroutine(UIManager.Instance.BossDefeated());
        yield return new WaitForSeconds(2f);
        SetUpGame();
    }


    [Serializable]
    public class Boss
    {
        public GameObject bossPrefab;
        public string bossName;
    }
}

