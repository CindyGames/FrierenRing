using UnityEngine;

public class WeaponChest : MonoBehaviour
{
    public GameObject abilityChoiceUI; // UI для выбора способности
    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            OpenChest();
            other.GetComponent<MobileCharacterController>().stopMove = true;
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        // Здесь можем добавить анимацию открытия сундука
        Debug.Log("Сундук открыт!");
        ShowAbilityChoices();

        GetComponent<Animator>().SetBool("is open", true);
    }

    private void ShowAbilityChoices()
    {
        // Активируем UI для выбора способностей
        abilityChoiceUI.SetActive(true);
    }
}
