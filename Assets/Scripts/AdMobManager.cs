using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager Instance;

    private InterstitialAd interstitialAd;


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
}
