using UnityEngine;

public class WinActivate : MonoBehaviour
{
    public GameObject winScreen, defeatScreen;

    public void WinScreenActivate()
    {
        winScreen.SetActive(true);
    }

    public void DefScreenActivate()
    {
        defeatScreen.SetActive(true);
    }
}
