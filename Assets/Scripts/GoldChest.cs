using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoldChest : MonoBehaviour
{
    public int minGoldAmount = 50; // ����������� ���������� ������
    public int maxGoldAmount = 150; // ������������ ���������� ������
    public TextMeshProUGUI goldText; // ����� ��� ����������� ����������� ������

    private bool isOpened = false; // ��������, ������ �� ������
    private int goldAmount; // ��������� ���������� ������, ������� ������ ��� ������
    private Chest chest;
    void Start()
    {
        chest = GetComponent<Chest>();
        if (goldText != null)
        {
            goldText.gameObject.SetActive(false); // �������� ����� � ������� � ������
        }

        // ��������� ���������� ���������� ������ � ��������� �� minGoldAmount �� maxGoldAmount
        goldAmount = Random.Range(minGoldAmount, maxGoldAmount + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ���� ����� �������� �������
        if (other.CompareTag("Player") && !isOpened)
        {
            isOpened = true; // ��������, ��� ������ ��� ������
            StartCoroutine(OpenChestAfterDelay(other));
            other.GetComponent<MobileCharacterController>().stopMove = true;

        }
    }

    // ����������� ��� �������� ������� � ���������� ������ ����� 2 �������
    IEnumerator OpenChestAfterDelay(Collider2D player)
    {
        chest.IsOpened = true;

        yield return new WaitForSeconds(2f); // ���� 2 �������


        player.GetComponent<PlayerGold>().AddGold(goldAmount);


        // ���������� ����� � �������
        if (goldText != null)
        {
            goldText.text = goldAmount.ToString();
            goldText.gameObject.SetActive(true);
        }

        //Debug.Log("Player received " + goldAmount + " gold.");
    }
}
