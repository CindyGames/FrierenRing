using System.Collections;
using TMPro;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    [SerializeField]
    int minAddValue, maxAddValue;

    private int power; // ���� �����
    public TextMeshProUGUI powerText; // ��������� UI ������� ��� ����������� ����
    public GameObject defeatEffect; // ������ ��������� (��������, �������� ������)

    public GameObject wictoryScreen;

    private void Start()
    {
        powerText = GetComponentInChildren<TextMeshProUGUI>();
        // ��������� �������� ��� ����������
        power = Random.Range(minAddValue, maxAddValue);
        UpdatePowerUI();
    }

    // ��������� UI � ������� ����� �����
    private void UpdatePowerUI()
    {
        powerText.text = power.ToString();
    }

    // ����� ��� ��������� ����� ���� (��������, ����� ����� �������� ���� �����)
    public void SetPower(int newPower)
    {
        power = newPower;
        UpdatePowerUI();
    }

    // �������������� � ������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                // ��������� �����������, ������� �������� ����� ����� 2 �������
                StartCoroutine(InvokeMethodAfterDelay(characterStats));

            }

            other.GetComponent<MobileCharacterController>().stopMove = true;


        }
    }

    // ���������� �����: ����� ��� ���� ���������
    private void ResolveFight(CharacterStats characterStats)
    {
        if (characterStats.strength >= power)
        {
            // ����� ���������, �������� ���� �����
            //characterStats.IncreasePower(power);
            DefeatEnemy();
        }
        else if (characterStats.strength < power)
        {
            // ����� �����������
            //characterStats.Die();
        }
    }

    // ����� ��� ��������� �����
    private void DefeatEnemy()
    {
        if (defeatEffect != null)
        {
            Instantiate(defeatEffect, transform.position, Quaternion.identity);
        }
        powerText.text = "0";
        //Destroy(gameObject); // ������� ����� �� ����

        if (wictoryScreen != null)
        {
            wictoryScreen.SetActive(true);
        }

    }



    // �����������, ������� ��� 2 ������� ����� ������� ������
    IEnumerator InvokeMethodAfterDelay(CharacterStats characterStats)
    {
        yield return new WaitForSeconds(2f); // ��� 2 �������
        ResolveFight(characterStats); // ����� ������ ����� ��������
    }


}
