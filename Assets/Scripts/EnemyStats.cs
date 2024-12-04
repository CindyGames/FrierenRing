using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField]
    int minAddValue, maxAddValue;

    public int power; // ���� ����� (��������)
    public TextMeshProUGUI powerText; // ��������� UI ������� ��� ����������� ����
    public GameObject defeatEffect; // ������ ��������� (��������, �������� ������)

    public int goldAmount = 1;
    private AudioSource audioClip;
    public AudioClip Audio;
    
    private void Start()
    {
        audioClip = GetComponent<AudioSource>();    

        powerText = GetComponentInChildren<TextMeshProUGUI>();
        // ��������� �������� ��� ����������
        
        UpdatePowerUI();
    }
    public void GetHp()
    {
        power = Random.Range(minAddValue, maxAddValue);
    }
    // ��������� UI � ������� ����� �����
    private void UpdatePowerUI()
    {
        powerText.text = power.ToString();
    }

    // ����� ��� ���������� ���� ����� ��� ��������� �������
    public void TakeDamage(int damageAmount)
    {
        power -= damageAmount;
        UpdatePowerUI();
        audioClip.PlayOneShot(Audio);

        // ���� ���� (��������) ����� �������� 0 ��� ������, �� �������
        if (power <= 0)
        {
            DefeatEnemy();
        }
    }

    // ����� ��� ���������� �������������� � ������
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

    // ���������� ����� ��� ������������ � �������
    private void ResolveFight(CharacterStats characterStats)
    {
        // ����� � ���� ��������� ���� ���� �����
        characterStats.DecreasePower(power); // ��������� ���� ������ �� �������� ���� �����
        DefeatEnemy(); // ���� �������� ����� ������������
    }

    // ����� ��� ��������� �����
    private void DefeatEnemy()
    {
        if (defeatEffect != null)
        {
            Instantiate(defeatEffect, transform.position, Quaternion.identity); // ������� ������ ���������
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerGold>().AddGold(goldAmount);

        Destroy(gameObject, 0.1f); // ������� ����� �� ����
    }
}
