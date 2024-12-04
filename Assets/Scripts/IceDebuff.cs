using UnityEngine;
using System.Collections;

public class IceDebuff : MonoBehaviour
{
    public float freezeDuration = 3f; // Длительность заморозки игрока
    public Vector3 iceOffset = new Vector3(0, 1f, 0); // Смещение льда относительно игрока
    public int damagePerSecond = 1; // Урон, наносимый каждую секунду

    private MobileCharacterController playerController; // Ссылка на контроллер игрока
    private CharacterStats playerStats; // Ссылка на скрипт со статистикой игрока
    private Coroutine freezeCoroutine; // Хранит текущую корутину заморозки
    private static IceDebuff activeDebuff; // Ссылка на активный дебаф

    private void Start()
    {
        // Находим контроллер игрока и его статистику
        playerController = GetComponentInParent<MobileCharacterController>();
        playerStats = GetComponentInParent<CharacterStats>();

        if (playerController != null && playerStats != null)
        {
            // Если уже есть активный дебаф, обновляем его время действия
            if (activeDebuff != null)
            {
                activeDebuff.RefreshDebuff();
                Destroy(gameObject); // Уничтожаем новый дебаф, так как обновили старый
                return;
            }

            // Если активного дебафа нет, устанавливаем его
            activeDebuff = this;
            StartDebuff();
        }
        else
        {
            Debug.LogWarning("PlayerController или CharacterStats не найдены!");
            Destroy(gameObject); // Уничтожаем дебаф, если нет игрока
        }

        // Устанавливаем позицию льда с нужным смещением
        transform.localPosition = iceOffset;
    }

    // Метод для начала заморозки
    private void StartDebuff()
    {
        freezeCoroutine = StartCoroutine(ApplyFreeze());
    }

    // Метод для обновления времени действия дебафа
    public void RefreshDebuff()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine); // Останавливаем текущую корутину заморозки
        }

        freezeCoroutine = StartCoroutine(ApplyFreeze()); // Перезапускаем заморозку с обновленным временем
    }

    // Корутина для заморозки игрока и нанесения урона каждую секунду
    private IEnumerator ApplyFreeze()
    {
        // Отключаем движение игрока влево и вправо
        playerController.stopUpward = true;

        float elapsedTime = 0f;

        // Пока продолжается заморозка
        while (elapsedTime < freezeDuration)
        {
            // Наносим урон каждую секунду
            playerStats.DecreasePower(damagePerSecond);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }

        // Восстанавливаем движение игрока
        playerController.stopUpward  = false;

        // Убираем текущий дебаф
        activeDebuff = null;

        // Уничтожаем объект льда (дебаф)
        Destroy(gameObject);
    }
}
