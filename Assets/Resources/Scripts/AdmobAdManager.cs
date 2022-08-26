using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobAdManager : MonoBehaviour
{
    private BannerView bannerView;
    private RewardedAd rewardedAd;
    //string BannerADID = "ca-app-pub-4580686436354709/1812494792";
    //string RewardADID = "ca-app-pub-4580686436354709/2189282209";
    string BannerADID = "ca-app-pub-3940256099942544/6300978111";
    string RewardADID = "ca-app-pub-3940256099942544/5224354917";


    public bool reward;
    
    // Start is called before the first frame update
    void Start()
    {
        reward = false;

        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestReward();
        bannerView.Show();
    }

    void RequestBanner()
    {
        AdSize adSize = new AdSize(320,50);
        this.bannerView = new BannerView(BannerADID, adSize, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    void RequestReward()
    {
        this.rewardedAd = new RewardedAd(RewardADID);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += this.HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += this.HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += this.HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += this.HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += this.HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }
    
    /*
     * 배너 관련
     */ 
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    /*
     * 리워드 관련
     */
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");

        this.RequestReward();

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //string type = args.Type;
        ////double amount = args.Amount;
        ////MonoBehaviour.print(
        ////    "HandleRewardedAdRewarded event received for "
        ////                + amount.ToString() + " " + type);

        reward = true;

    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
            RequestReward();
        }
        else
        {
            RequestReward();
        }
    }


}
