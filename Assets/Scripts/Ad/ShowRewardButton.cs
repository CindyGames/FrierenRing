using UnityEngine;
using UnityEngine.UI;

public class ShowRewardButton : MonoBehaviour
{
    public int live = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        //int live = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().extraLive;
        if (live > 0)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().extraLive--;

            GameObject adObject = GameObject.FindGameObjectWithTag("Reward Ad");
            if (adObject != null)
            {
                live--;

                Button bt = adObject.GetComponent<RewardedAdController>().button;
                bt.gameObject.SetActive(true);
            }
        }
    }

}
