using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField]
    int minAddValue, maxAddValue;

    public int power; // Сила врага (здоровье)
    public TextMeshProUGUI powerText; // Текстовый UI элемент для отображения силы
    public GameObject defeatEffect; // Эффект поражения (например, анимация смерти)

    public int goldAmount = 1;
    private AudioSource audioClip;
    public AudioClip Audio;
    
    private void Start()
    {
        audioClip = GetComponent<AudioSource>();    

        powerText = GetComponentInChildren<TextMeshProUGUI>();
        // Случайное значение для добавления
        
        UpdatePowerUI();
    }
    public void GetHp()
    {
        power = Random.Range(minAddValue, maxAddValue);
    }
    // Обновляем UI с текущей силой врага
    private void UpdatePowerUI()
    {
        powerText.text = power.ToString();
    }

    // Метод для уменьшения силы врага при попадании снаряда
    public void TakeDamage(int damageAmount)
    {
        power -= damageAmount;
        UpdatePowerUI();
        audioClip.PlayOneShot(Audio);

        // Если сила (здоровье) врага достигла 0 или меньше, он умирает
        if (power <= 0)
        {
            DefeatEnemy();
        }
    }

    // Метод для разрешения взаимодействия с героем
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                ResolveFight(characterStats);
            }

        }
    }

    // Разрешение битвы при столкновении с игроком
    private void ResolveFight(CharacterStats characterStats)
    {
        // Герой и враг уменьшают силы друг друга
        characterStats.DecreasePower(power); // Уменьшаем силу игрока на значение силы врага
        DefeatEnemy(); // Враг погибает после столкновения
    }

    // Метод для поражения врага
    private void DefeatEnemy()
    {
        if (defeatEffect != null)
        {
            Instantiate(defeatEffect, transform.position, Quaternion.identity); // Создаем эффект поражения
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerGold>().AddGold(goldAmount);

        Destroy(gameObject, 0.1f); // Удаляем врага из игры
    }
}
