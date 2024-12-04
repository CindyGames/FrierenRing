using UnityEngine;

public class DisableOnTouchOrKeyPress : MonoBehaviour
{
    void Update()
    {
        // Проверка на нажатие экрана на мобильных устройствах
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DisableObject();
        }

        // Проверка на нажатие клавиш A или D на клавиатуре
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            DisableObject();
        }
    }

    void DisableObject()
    {
        gameObject.SetActive(false); // Отключаем игровой объект
    }
}
