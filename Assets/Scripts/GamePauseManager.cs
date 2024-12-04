using UnityEngine;
using UnityEngine.EventSystems; // Для работы с UI
using System.Collections.Generic; // Для работы с List

public class GamePauseManager : MonoBehaviour
{
    public bool isGamePaused = true;
    public MobileCharacterController characterController; // Ссылка на скрипт движения персонажа
    public List<GameObject> objectsToToggle; // Список объектов, которые нужно включать/выключать

    private Vector2 startTouchPosition;  // Начальная позиция касания
    private Vector2 endTouchPosition;    // Конечная позиция касания
    private float minSwipeDistance = 15f; // Минимальное расстояние для свайпа

    void Start()
    {
        // Начинаем игру с паузы
        PauseGame();
    }

    void Update()
    {
        if (isGamePaused)
        {
            // Проверка свайпа для мобильных устройств
            if (Input.touchCount > 0 && !IsPointerOverUI())
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                    CheckSwipe();
                }
            }

            // Проверка нажатия клавиш для ПК
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                ResumeGame();
            }
        }
    }

    // Метод для паузы игры
    public void PauseGame()
    {
        if (characterController != null)
        {
            characterController.stopMove = true; // Отключаем движение персонажа
        }

        isGamePaused = true;

        // Включаем UI объекты из списка
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(true);
        }
    }

    // Метод для продолжения игры
    public void ResumeGame()
    {
        if (characterController != null)
        {
            characterController.stopMove = false; // Включаем движение персонажа
        }

        isGamePaused = false;

        // Отключаем UI объекты из списка
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }
    }

    // Метод для проверки свайпа
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(endTouchPosition.x - startTouchPosition.x);

        if (swipeDistanceX > minSwipeDistance)
        {
            ResumeGame();
        }
    }

    // Метод для проверки касания по UI
    private bool IsPointerOverUI()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = touch.position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
        return false;
    }
}
