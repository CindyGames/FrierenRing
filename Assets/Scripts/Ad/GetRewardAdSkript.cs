using UnityEngine;
using UnityEngine.UI;


public class GetRewardAdSript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("Reward Ad");
        RewardedAdController ad = controller.GetComponent<RewardedAdController>();
        Button button = GetComponent<Button>();
        //button.onClick.AddListener(ad.ShowRewardedAd());
    }

}
