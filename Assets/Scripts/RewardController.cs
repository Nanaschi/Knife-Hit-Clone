using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RewardController : MonoBehaviour
{

    public static RewardController Instance;

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




    [SerializeField] int hoursToReward;
    [SerializeField] int minutesToReward;
    [SerializeField] int secondsToReward = 10;

    private const string NEXT_REWARD = "RewardTime";

    private int minReward = 20;
    private int maxReward = 60;

    public DateTime NextRewardTime => GetNextRewardTime();

    public TimeSpan TimeToReward { get => NextRewardTime.Subtract(DateTime.Now); }




    public bool CanReward()
    {
        return NextRewardTime <= DateTime.Now;
    }

    public int GetRandomReward()
    {
        return UnityEngine.Random.Range(minReward, maxReward);
    }

    public void ResetRewardTime()
    {
        DateTime nextReward = DateTime.Now.Add(new TimeSpan(hoursToReward, minutesToReward, secondsToReward));
        SaveNextRewardTime(nextReward);
    }
    public void SaveNextRewardTime(DateTime time)
    {
        PlayerPrefs.SetString(NEXT_REWARD, time.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    public DateTime GetNextRewardTime()
    {
        string nextReward = PlayerPrefs.GetString(NEXT_REWARD, string.Empty);


        if (!string.IsNullOrEmpty(nextReward))
        {
            return DateTime.FromBinary(Convert.ToInt64 (nextReward));
        } else
        {
            return DateTime.Now;
        }
    }
  

}
