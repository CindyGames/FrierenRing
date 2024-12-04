using UnityEngine;

public class RaceSelectionDialog : MonoBehaviour
{
    public GameObject raceSelectionPanel;  // UI-панель для выбора расы

    private void Start()
    {
        // Скрываем панель по умолчанию
        raceSelectionPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, столкнулся ли игрок с триггером
        if (other.CompareTag("Player"))
        {
            // Открываем панель выбора расы
            raceSelectionPanel.SetActive(true);
            // Останавливаем движение игрока, если нужно
            other.GetComponent<MobileCharacterController>().stopMove = true;
        }
    }

}
