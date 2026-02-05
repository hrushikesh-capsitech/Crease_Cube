using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager Instance;

    private InterstitialAd interstitialAd;

    private RewardedAd rewardedAd;

    public enum RewardAction
    {
        None = 0,
        SkipToNextCube,
        RetryGame
    }

    private RewardAction currentRewardAction = RewardAction.None;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAdMob();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAdMob()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob Initialized");

            LoadInterstitial();
            LoadRewardedAd();
        });
    }


    public void LoadInterstitial()
    {
        string adUnitId = "ca-app-pub-XXXXXXXXXXXXXXXX/IIIIIIIIII";

        InterstitialAd.Load(adUnitId, new AdRequest(), (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial failed to load: " + error);
                return;
            }

            interstitialAd = ad;
            Debug.Log("Interstitial loaded");
        });
    }

    public void ShowInterstitial()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
    }


    public void LoadRewardedAd()
    {
        string adUnitId = "ca-app-pub-XXXXXXXXXXXXXXXX/RRRRRRRRRR"; // test ID for now

        RewardedAd.Load(adUnitId, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error);
                return;
            }

            rewardedAd = ad;
            Debug.Log("Rewarded ad loaded");
        });
    }


    public void ShowRewardedAd(RewardAction action)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            currentRewardAction = action;

            rewardedAd.Show(reward =>
            {
                Debug.Log("Reward earned: " + action);
                HandleReward();
            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }


    private void HandleReward()
    {
        switch (currentRewardAction)
        {
            case RewardAction.SkipToNextCube:
                ScoreManager.Instance.ExecuteJumpToNextCube();
                break;

            case RewardAction.RetryGame:
                GameManager.Instance.retryBtnOnClick();
                break;
        }

        currentRewardAction = RewardAction.None;

        LoadRewardedAd();
    }





}
