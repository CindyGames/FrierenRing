using TMPro;
using UnityEngine;

public class PowerBonus : MonoBehaviour
{
    public TextMeshProUGUI bonusText; // ������ �� UI ������� ��� ����������� ������
    public int minAddValue = 5; // ����������� �������� ����������
    public int maxAddValue = 15; // ������������ �������� ����������
    public int minMultiplier = 1; // ����������� ���������
    public int maxMultiplier = 2; // ������������ ���������
    [Range(0f, 1f)] public float additionChance = 0.7f; // ����������� ��������� ���������� (0.7 = 70%)
    private int bonusValue; // ������ �������� ������
    private bool isAddition; // ���������� ��� ������ (true - ����������, false - ���������)

    private void Start()
    {
        bonusText = GetComponent<TextMeshProUGUI>();
        // ��������� ����� ����� ����������� ��� ���������� �� ������ �����������
        isAddition = Random.value < additionChance;

        if (isAddition)
        {
            // ��������� �������� ��� ����������
            bonusValue = Random.Range(minAddValue, maxAddValue);
            bonusText.text = "+" + bonusValue.ToString();
        }
        else
        {
            // ��������� �������� ��� ���������
            bonusValue = Random.Range(minMultiplier, maxMultiplier);
            bonusText.text = "x" + bonusValue.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                ApplyBonus(characterStats);
            }

            // ������� ����� ����� �������������
            Destroy(gameObject);
        }
    }

    private void ApplyBonus(CharacterStats characterStats)
    {
        if (isAddition)
        {
            // ��������� ��������� ����� � ��������
            characterStats.IncreasePower(bonusValue);
        }
        else
        {
            // �������� �������� �� ��������� �����
            characterStats.MultiplyPower(bonusValue);
        }
    }
}
