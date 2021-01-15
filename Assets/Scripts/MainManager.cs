using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    [SerializeField] Image selectedKnife;


    private string gameURL = "https://play.google.com/store/apps/details?id=";
    private string appID = "com.ketchapp.knifehit&hl=en&gl=US";

    public Image SelectedKnife { get => selectedKnife; }

    public void Play()
    {
        SceneManager.LoadScene("Game");
        SoundManager.Instance.PlayButton();
    }


    public void Rate()
    {
        Application.OpenURL(gameURL + appID);
        SoundManager.Instance.PlayButton();
    }
}
