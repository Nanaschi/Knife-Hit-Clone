using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] GameObject rewardPanel;
    [SerializeField] ParticleSystem applePS;
    [SerializeField] Text rewardText;



    private void Update()
    {
        if (RewardController.Instance.CanReward())
        {
            rewardText.text = "Get Reward!";

        }else
        {
            TimeSpan timeToReward = RewardController.Instance.TimeToReward;
            rewardText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds); 
        }
    }



    public void RewardPlayer()
    {
        if (RewardController.Instance.CanReward())
        {
            int amount = RewardController.Instance.GetRandomReward();


            StartCoroutine(RewardPanel());
            RewardController.Instance.ResetRewardTime();
            SoundManager.Instance.AppleRewardClip();
            GameManager.Instance.TotalApple += amount;

        }
    }



    private IEnumerator RewardPanel()
    {
        rewardPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        Instantiate(applePS);
        yield return new WaitForSeconds(3f);
        //soundManager.PlayeSound()
        rewardPanel.SetActive(false);
    }
}
