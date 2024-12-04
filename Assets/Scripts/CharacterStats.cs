using TMPro;  // ��� ������ � TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement;  // ��� ������ �� �������

public class CharacterStats : MonoBehaviour
{
    public int extraLive = 1;

    public int strength = 1; // ���������� ���� ���������

    public TextMeshProUGUI strengthText; // ������ �� ����� ��� ������� ���������


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
        UpdateStrengthText(); // ��������� ����������� ������ ���� ����� ���������

        collider.enabled = true;
    }

    // ����� ��� ���������� ��������
    public void IncreasePower(int amount)
    {
        strength += amount;
        UpdateStrengthText();
        audioSourceIncrease.Play();
    }

    // ����� ��� ��������� ��������
    public void MultiplyPower(int multiplier)
    {
        strength *= multiplier;
        UpdateStrengthText();
        audioSourceIncrease.Play();
    }

    // ��������� �����, ��������� ���� ���������
    void UpdateStrengthText()
    {
        if (strength <= 0)
        {
            strength = 0;

            Die(); // ���� ���� 0 ��� ������, �������� "�������"
        }
        if (strengthText != null)
        {
            strengthText.text = strength.ToString();
        }

    }

    // ����� ��� ���������� ���� (� ������, ����� �������� �������� � �������)
    public void DecreasePower(int amount)
    {
        strength -= amount;
        UpdateStrengthText();

        animator.SetTrigger("damage");
        // ��������������� �����
        audioSourceDamage.Play();

    }

    // ����� ��� ������� ���� (� ������, ����� �������� �������� � �������)
    public void DividePower(int value)
    {

        strength /= value;
        UpdateStrengthText(); // ��������� ����������� ���� ����� ���������

        animator.SetTrigger("damage");
        // ��������������� �����
        audioSourceDamage.Play();

    }

    // ����� ��� ��������� ������ ���������
    void Die()
    {
        Debug.Log("You lose!");

        // ������������� ����� � ����
        mobileCharacterController.stopMove = true;
        //GetComponent<PlayerShooting>().stopShoot = true;
        GetComponent<BoxCollider2D>().enabled = false;

        GameObject.FindGameObjectWithTag("Win Screen").GetComponent<WinActivate>().DefScreenActivate();

        GetComponentInChildren<PlayerWeaponManager>().DisableAllWeapons();

        collider.enabled = false;
    }


}
