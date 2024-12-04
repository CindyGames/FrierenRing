/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class StickyBannerController : MonoBehaviour
{

    private Banner banner;

    private void Start()
    {
        RequestBanner();

    }

    private void RequestBanner()
    {

        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        string adUnitId = "R-M-12483966-1"; //"demo-banner-yandex"; // "R-M-12483966-1";  // замените на "R-M-XXXXXX-Y"

        if (this.banner != null)
        {
            this.banner.Destroy();
        }

        // Set sticky banner width
        BannerAdSize bannerSize = BannerAdSize.StickySize(GetScreenWidthDp());
        // Or set inline banner maximum width and height
        // BannerAdSize bannerSize = BannerAdSize.InlineSize(GetScreenWidthDp(), 300);
        this.banner = new Banner(adUnitId, bannerSize, AdPosition.BottomCenter);


        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.banner.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.banner.OnLeftApplication += this.HandleLeftApplication;
        this.banner.OnAdClicked += this.HandleAdClicked;
        this.banner.OnImpression += this.HandleImpression;

        this.banner.LoadAd(this.CreateAdRequest());
    }

    // Example how to get screen width for request
    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }


    #region Banner callback handlers

    private void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("AdLoaded event received");
        banner.Show();
    }

    private void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log($"AdFailedToLoad event received with message: {args.Message}");
        // Настоятельно не рекомендуется пытаться загрузить новое объявление с помощью этого метода
    }

    private void HandleLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("LeftApplication event received");
    }

    private void HandleReturnedToApplication(object sender, EventArgs args)
    {
        Debug.Log("ReturnedToApplication event received");
    }

    private void HandleAdClicked(object sender, EventArgs args)
    {
        Debug.Log("AdClicked event received");
    }

    private void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        Debug.Log($"HandleImpression event received with data: {data}");
    }

    #endregion
}
