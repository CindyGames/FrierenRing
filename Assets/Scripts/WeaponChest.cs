using UnityEngine;

public class WeaponChest : MonoBehaviour
{
    public GameObject abilityChoiceUI; // UI ��� ������ �����������
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
        // ����� ����� �������� �������� �������� �������
        Debug.Log("������ ������!");
        ShowAbilityChoices();

        GetComponent<Animator>().SetBool("is open", true);
    }

    private void ShowAbilityChoices()
    {
        // ���������� UI ��� ������ ������������
        abilityChoiceUI.SetActive(true);
    }
}
