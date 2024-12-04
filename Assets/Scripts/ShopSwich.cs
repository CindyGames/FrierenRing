using UnityEngine;

public class ShopSwich : MonoBehaviour
{
    public GameObject shop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
                shop.gameObject.SetActive(true);
    }
    void Start()
    {

        shop.gameObject.SetActive(false);

    }


}
