using UnityEngine;
using System.Collections;

public class AbilityPickup : MonoBehaviour
{
    private BoxCollider2D boxCollider; // Ссылка на коллайдер

    private void Start()
    {
        // Находим коллайдер и отключаем его до взаимодействия
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        // Проверка касания для мобильных устройств
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                CheckTouch(touchPosition);
            }
        }

        // Проверка нажатия для ПК (клик мыши)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CheckTouch(mousePosition);
        }
    }

    private void CheckTouch(Vector3 position)
    {
        Vector2 touchPos2D = new Vector2(position.x, position.y);

        // Проверка касания по коллайдеру напрямую, если он активен
        if (boxCollider != null && boxCollider.OverlapPoint(touchPos2D))
        {
            OnPickup();
        }
    }

    private void OnPickup()
    {
        // Применение улучшения оружия
        var weaponUpgrade = GetComponentInChildren<WeaponUpgradeBonus>();
        if (weaponUpgrade != null)
        {
            weaponUpgrade.ApplyUpgrade();
        }
        else
        {
            var levelBonus = GetComponentInChildren<WeaponLevelBonus>();
            if (levelBonus != null) levelBonus.ApplyUpgrade();
        }

        // Отключение connectedDoor
        var doorController = GetComponent<DoorController>();
        if (doorController != null && doorController.connectedDoor != null)
        {
            doorController.connectedDoor.SetActive(false);
        }
        else
        {
            Debug.LogWarning("DoorController или connectedDoor не найден!");
        }

        // Запуск корутины с задержкой для активации экрана победы
        StartCoroutine(ActivateWinScreenWithDelay());

        // Удаление объекта после взаимодействия
        //Destroy(gameObject);
    }

    private IEnumerator ActivateWinScreenWithDelay()
    {
        // Задержка в 1 секунду
        yield return new WaitForSeconds(1f);

        // Активация экрана победы
        var winScreenObject = GameObject.FindGameObjectWithTag("Win Screen");
        if (winScreenObject != null)
        {
            var winScreen = winScreenObject.GetComponent<WinActivate>();
            if (winScreen != null)
            {
                winScreen.WinScreenActivate();
            }
            else
            {
                Debug.LogWarning("WinActivate не найден на объекте Win Screen!");
            }
        }
        else
        {
            Debug.LogWarning("Объект с тегом 'Win Screen' не найден!");
        }
    }
}
