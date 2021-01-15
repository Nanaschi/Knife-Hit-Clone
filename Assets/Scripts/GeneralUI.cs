using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{

    public static GeneralUI Instance;

    [Header ("Panels")]
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject shopPanel;

    [Header("Settings UI")]
    [SerializeField] GameObject soundON;
    [SerializeField] GameObject soundOFF;
    [SerializeField] GameObject vibrationON;
    [SerializeField] GameObject vibrationOFF;


    [SerializeField] Text totalApple;



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


    // Update is called once per frame
    void Update()
    {
        totalApple.text = GameManager.Instance.TotalApple.ToString();

        UpdateSondsUI();
        UpdateVibrationUI();
    }


    public void SoundsOnOff()
    {
        SoundManager.Instance.PlayButton();
    if (GameManager.Instance.SoundSettings)
        {
            GameManager.Instance.SoundSettings = false;
        }
        else
        {
            GameManager.Instance.SoundSettings = true;
        }
    }


    public void VibrationsOnOff()
    {
        SoundManager.Instance.PlayButton();
        if (GameManager.Instance.VibrationSettings)
        {
            GameManager.Instance.VibrationSettings = false;
        }
        else
        {
            GameManager.Instance.VibrationSettings = true;
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        SoundManager.Instance.PlayButton();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        SoundManager.Instance.PlayButton();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        SoundManager.Instance.PlayButton();
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        SoundManager.Instance.PlayButton();

    }

    private void UpdateSondsUI()
    {
        if (GameManager.Instance.SoundSettings)
        {
            soundON.SetActive(true);
            soundOFF.SetActive(false);
        }
        else
        {
            soundON.SetActive(false);
            soundOFF.SetActive(true);
        }
    }
    private void UpdateVibrationUI()
    {
        if (GameManager.Instance.VibrationSettings)
        {
            vibrationON.SetActive(true);
            vibrationOFF.SetActive(false);
        }
        else
        {
            vibrationON.SetActive(false);
            vibrationOFF.SetActive(true);
        }
    }
}
