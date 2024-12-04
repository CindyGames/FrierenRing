using TMPro;  // Для работы с TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement;  // Для работы со сценами

public class CharacterStats : MonoBehaviour
{
    public int extraLive = 1;

    public int strength = 1; // Переменная силы персонажа

    public TextMeshProUGUI strengthText; // Ссылка на текст над головой персонажа


    MobileCharacterController mobileCharacterController;
    Animator animator;
    public AudioSource audioSourceDamage;
    public AudioSource audioSourceIncrease;

    private Collider2D collider;

    void Start()
    {
        mobileCharacterController = GetComponent<MobileCharacterController>();
        animator = GetComponent<Animator>();

        UpdateStrengthText();

        collider = GetComponent<Collider2D>();
    }

    public void SetPower(int newPower)
    {
        strength = newPower;
        UpdateStrengthText(); // Обновляем отображение текста силы после изменения

        collider.enabled = true;
    }

    // Метод для увеличения мощности
    public void IncreasePower(int amount)
    {
        strength += amount;
        UpdateStrengthText();
        audioSourceIncrease.Play();
    }

    // Метод для умножения мощности
    public void MultiplyPower(int multiplier)
    {
        strength *= multiplier;
        UpdateStrengthText();
        audioSourceIncrease.Play();
    }

    // Обновляем текст, выводящий силу персонажа
    void UpdateStrengthText()
    {
        if (strength <= 0)
        {
            strength = 0;

            Die(); // Если сила 0 или меньше, персонаж "умирает"
        }
        if (strengthText != null)
        {
            strengthText.text = strength.ToString();
        }

    }

    // Метод для уменьшения силы (в случае, когда персонаж попадает в ловушку)
    public void DecreasePower(int amount)
    {
        strength -= amount;
        UpdateStrengthText();

        animator.SetTrigger("damage");
        // Воспроизведение звука
        audioSourceDamage.Play();

    }

    // Метод для деления силы (в случае, когда персонаж попадает в ловушку)
    public void DividePower(int value)
    {

        strength /= value;
        UpdateStrengthText(); // Обновляем отображение силы после изменения

        animator.SetTrigger("damage");
        // Воспроизведение звука
        audioSourceDamage.Play();

    }

    // Метод для обработки смерти персонажа
    void Die()
    {
        Debug.Log("You lose!");

        // Останавливаем время в игре
        mobileCharacterController.stopMove = true;
        //GetComponent<PlayerShooting>().stopShoot = true;
        GetComponent<BoxCollider2D>().enabled = false;

        GameObject.FindGameObjectWithTag("Win Screen").GetComponent<WinActivate>().DefScreenActivate();

        GetComponentInChildren<PlayerWeaponManager>().DisableAllWeapons();

        collider.enabled = false;
    }


}
