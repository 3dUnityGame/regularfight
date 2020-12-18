using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class SoundMute : MonoBehaviour
{
    public GameObject adNotReady;

    private BannerView bannerAd;
    private RewardedAd rewardedAd;
    private string bannerID = "ca-app-pub-9464568626154470/8193203681";
    private string rewardedID = "ca-app-pub-9464568626154470/4370682201";


    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        CreateAndLoadRewardedAd();

        if (PlayerPrefs.GetInt("mute") == 1)
        {
            GetComponent<AudioSource>().Stop();
        }
        Banner();
    }

    private void CreateAndLoadRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedID);
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 25);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CreateAndLoadRewardedAd();
        bannerAd.Show();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        adNotReady.SetActive(false);
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            bannerAd.Hide();
            rewardedAd.Show();
            adNotReady.SetActive(true);
        }
    }

    public void Banner()
    {
        bannerAd = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest yeniReklam = new AdRequest.Builder().Build();

        bannerAd.LoadAd(yeniReklam);
    }

    public void DestroyBanner()
    {
        bannerAd.Destroy();
    }

    public void MuteUnmute()
    {
        if(PlayerPrefs.GetInt("mute") == 0)
        {
            GetComponent<AudioSource>().Stop();
            PlayerPrefs.SetInt("mute", 1);
        }
        else
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("mute", 0);
        }
    }
}
