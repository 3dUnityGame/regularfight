using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdScript : MonoBehaviour
{
    public GameObject adNotReady;

    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private string inters = "ca-app-pub-9464568626154470/1300847493";
    private string rewardedID = "ca-app-pub-9464568626154470/4370682201";

    void Start()
    {
        interstitial = new InterstitialAd(inters);
        interstitial.OnAdClosed += HandleOnAdClosed;
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);

        rewardedAd = new RewardedAd(rewardedID);
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest rewardRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(rewardRequest);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        HealthBar.hb.DoubleTheCoins();
        adNotReady.SetActive(true);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        adNotReady.SetActive(true);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        adNotReady.SetActive(false);
    }


    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        HealthBar.hb.watched = true;
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        HealthBar.hb.watched = true;
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            adNotReady.SetActive(true);
        }
    }

    public void IntesitialAdShow()
    {
        if(HealthBar.hb.watched == false)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }
    }
}
