using System;
using UnityEngine;
using UnityEngine.UI;
using YandexMobileAds;
using YandexMobileAds.Base;

public class RewardedAdController : MonoBehaviour
{

    private RewardedAdLoader rewardedAdLoader;
    private RewardedAd rewardedAd;
    public Button button;

    private void Start()
    {
        GameObject[] Reward = GameObject.FindGameObjectsWithTag("Reward Ad");

        if (Reward.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            SetupLoader();
            RequestRewardedAd();
            DontDestroyOnLoad(gameObject);

            button.onClick.AddListener(ShowRewardedAd);
        }

    }

    private void SetupLoader()
    {
        rewardedAdLoader = new RewardedAdLoader();
        rewardedAdLoader.OnAdLoaded += HandleAdLoaded;
        rewardedAdLoader.OnAdFailedToLoad += HandleAdFailedToLoad;
    }

    private void RequestRewardedAd()
    {
        string adUnitId = "R-M-12483966-2"; // "demo-rewarded-yandex"; // "R-M-12483966-2"; // замените на "R-M-XXXXXX-Y"
        AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(adUnitId).Build();
        rewardedAdLoader.LoadAd(adRequestConfiguration);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();
        }
    }

    public void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        // The ad was loaded successfully. Now you can handle it.
        rewardedAd = args.RewardedAd;

        // Add events handlers for ad actions
        rewardedAd.OnAdClicked += HandleAdClicked;
        rewardedAd.OnAdShown += HandleAdShown;
        rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
        rewardedAd.OnAdImpression += HandleImpression;
        rewardedAd.OnAdDismissed += HandleAdDismissed;
        rewardedAd.OnRewarded += HandleRewarded;
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // Ad {args.AdUnitId} failed for to load with {args.Message}
        // Attempting to load a new ad from the OnAdFailedToLoad event is strongly discouraged.
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        // Called when an ad is dismissed.

        // Clear resources after an ad dismissed.
        DestroyRewardedAd();

        // Now you can preload the next rewarded ad.
        RequestRewardedAd();
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        // Called when rewarded ad failed to show.

        // Clear resources after an ad dismissed.
        DestroyRewardedAd();

        // Now you can preload the next rewarded ad.
        RequestRewardedAd();
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        // Called when a click is recorded for an ad.
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
        // Called when an ad is shown.
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        // Called when an impression is recorded for an ad.
    }

    public void HandleRewarded(object sender, Reward args)
    {
        // Called when the user can be rewarded with {args.type} and {args.amount}.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterStats>().SetPower(100);
        player.GetComponentInChildren<PlayerWeaponManager>().ActivateCurrentWeapon();
        player.GetComponent<MobileCharacterController>().stopMove = false;

        button.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Win Screen").GetComponent<WinActivate>().defeatScreen.SetActive(false);
    }

    public void DestroyRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }
}
