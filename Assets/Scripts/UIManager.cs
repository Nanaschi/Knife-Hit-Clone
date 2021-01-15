using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;



    [Header("UI Settings")]
    [SerializeField] Text scoreText;
    [SerializeField] Text stageText;
    [SerializeField] GameObject stageContainer;
    [SerializeField] Color stageCompletedColor;
    [SerializeField] Color stageNormalColor;

    public List<Image> stageIcons;

    [Header("UI Boss")]

    [SerializeField] GameObject bossFight;
    [SerializeField] GameObject bossDefeated;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] Text gameOverScore;
    [SerializeField] Text gameOverStage;





    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        gameOverScore.text = GameManager.Instance.Score.ToString();

        stageText.text = "Stage " + GameManager.Instance.Stage;

        gameOverStage.text = "Stage " + GameManager.Instance.Stage;


        UpdateUI();
    }

    public IEnumerator BossStart()
    {
        bossFight.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossFight.SetActive(false);
    }


    public IEnumerator BossDefeated()
    {
        bossDefeated.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossDefeated.SetActive(false);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        stageContainer.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }


    public void OpenFacebook()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCmum02a595lyju1IndjKqog");
    }

    public void OpenShop()
    {
        GeneralUI.Instance.OpenShop();
    }

    public void OpenOptions()
    {
        GeneralUI.Instance.OpenSettings();
    }

    private void UpdateUI()
    {
        if (GameManager.Instance.Stage % 5 == 0)
        {
            foreach (var icon in stageIcons)
            {
                icon.gameObject.SetActive(false);
                stageIcons[stageIcons.Count - 1].color = stageNormalColor;
                stageText.text = LevelManager.Instance.BossName;
            }
        }
        else
        {
            for (int i = 0; i < stageIcons.Count; i++)
            {
                stageIcons[i].gameObject.SetActive(true);

                stageIcons[i].color = GameManager.Instance.Stage % 5 <= i ? stageNormalColor : stageCompletedColor;
            }
        }
    }


}
