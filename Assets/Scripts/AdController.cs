using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class AdController : MonoBehaviour
{
    private Banner banner;
    private Interstitial interstitial;
    private InterstitialAdLoader interAdLoad;
    private void Awake()
    {
        interAdLoad = new InterstitialAdLoader();
        interAdLoad.OnAdLoaded += HandleInterstitialLoaded;
        requestInterstitial();
        requestBanner();
    }
    private void requestInterstitial()
    {
        string interId = "R-M-16503694-1";
        AdRequestConfiguration adReqestConf = new AdRequestConfiguration.Builder(interId).Build();
        interAdLoad.LoadAd(adReqestConf);
    }
    public void showInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Show();
        }
    }
    public void HandleInterstitialLoaded(object sender, InterstitialAdLoadedEventArgs args)
    {
        interstitial = args.Interstitial;
        interstitial.OnAdFailedToShow += HandleFailedToShow;
        interstitial.OnAdDismissed += HandleDismissed;
    }

    private void HandleDismissed(object sender, EventArgs e)
    {
        requestInterstitial();
    }

    private void HandleFailedToShow(object sender, AdFailureEventArgs e)
    {
        requestInterstitial();
    }

    private void requestBanner()
    {
        string Id = "R-M-16503694-2";
        BannerAdSize bannerMaxSize = BannerAdSize.StickySize(GetScreenWithDp());
        banner = new Banner(Id, bannerMaxSize, AdPosition.BottomCenter);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }

    private int GetScreenWithDp()
    {
        int screnWidth = (int)Screen.safeArea.width-950;
        return ScreenUtils.ConvertPixelsToDp(screnWidth);
    }
    private void bannerAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("adLoaded");
        banner.Show();
    }
}
