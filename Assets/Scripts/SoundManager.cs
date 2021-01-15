using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [SerializeField] AudioClip buttonClip;
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip appleHitClip;
    [SerializeField] AudioClip appleRewardClip;
    [SerializeField] AudioClip wheelClip;
    [SerializeField] AudioClip knifeClip;
    [SerializeField] AudioClip knifeFireClip;
    [SerializeField] AudioClip unlockedClip;
    [SerializeField] AudioClip fightStartClip;
    [SerializeField] AudioClip fightEndClip;
    


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
        DontDestroyOnLoad(gameObject);
    }

    private void PlaySound (AudioClip clip, float vol = 1)
    {
        if (GameManager.Instance.SoundSettings)
        {
            if (Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, vol);
            }
        }
    }

    public void PlayButton()
    {
        PlaySound(buttonClip);
    }
    public void PlayAppleHit()
    {
        PlaySound(appleHitClip);
    }
    public void PlayWheelHit()
    {
        PlaySound(wheelClip);
    }
    public void PlayeKnifeHit()
    {
        PlaySound(knifeClip);
    }
    public void PlayeKnifeFire()
    {
        PlaySound(knifeFireClip);
    }
    public void PlayUnlock()
    {
        PlaySound(unlockedClip);
    }
    public void PlayBossStartFight()
    {
        PlaySound(fightStartClip);
    }
    public void PlayBossEndFight()
    {
        PlaySound(fightEndClip);
    }
    public void PlayGameOverClip()
    {
        PlaySound(gameOverClip);
    }
    public void AppleRewardClip()
    {
        PlaySound(appleRewardClip);
    }

    public void Vibrate()
    {
        if (GameManager.Instance.VibrationSettings)
        {
            Handheld.Vibrate();
        }
    }

}
