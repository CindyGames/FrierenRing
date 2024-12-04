using UnityEngine;

public class HideDeffitPanel : MonoBehaviour
{
    public GameObject winScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (winScreen == null && winScreen.activeSelf)
        {
            gameObject.SetActive(false);
        }

    }


}
