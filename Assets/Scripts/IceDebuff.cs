using UnityEngine;
using System.Collections;

public class IceDebuff : MonoBehaviour
{
    public float freezeDuration = 3f; // ������������ ��������� ������
    public Vector3 iceOffset = new Vector3(0, 1f, 0); // �������� ���� ������������ ������
    public int damagePerSecond = 1; // ����, ��������� ������ �������

    private MobileCharacterController playerController; // ������ �� ���������� ������
    private CharacterStats playerStats; // ������ �� ������ �� ����������� ������
    private Coroutine freezeCoroutine; // ������ ������� �������� ���������
    private static IceDebuff activeDebuff; // ������ �� �������� �����

    private void Start()
    {
        // ������� ���������� ������ � ��� ����������
        playerController = GetComponentInParent<MobileCharacterController>();
        playerStats = GetComponentInParent<CharacterStats>();

        if (playerController != null && playerStats != null)
        {
            // ���� ��� ���� �������� �����, ��������� ��� ����� ��������
            if (activeDebuff != null)
            {
                activeDebuff.RefreshDebuff();
                Destroy(gameObject); // ���������� ����� �����, ��� ��� �������� ������
                return;
            }

            // ���� ��������� ������ ���, ������������� ���
            activeDebuff = this;
            StartDebuff();
        }
        else
        {
            Debug.LogWarning("PlayerController ��� CharacterStats �� �������!");
            Destroy(gameObject); // ���������� �����, ���� ��� ������
        }

        // ������������� ������� ���� � ������ ���������
        transform.localPosition = iceOffset;
    }

    // ����� ��� ������ ���������
    private void StartDebuff()
    {
        freezeCoroutine = StartCoroutine(ApplyFreeze());
    }

    // ����� ��� ���������� ������� �������� ������
    public void RefreshDebuff()
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine); // ������������� ������� �������� ���������
        }

        freezeCoroutine = StartCoroutine(ApplyFreeze()); // ������������� ��������� � ����������� ��������
    }

    // �������� ��� ��������� ������ � ��������� ����� ������ �������
    private IEnumerator ApplyFreeze()
    {
        // ��������� �������� ������ ����� � ������
        playerController.stopUpward = true;

        float elapsedTime = 0f;

        // ���� ������������ ���������
        while (elapsedTime < freezeDuration)
        {
            // ������� ���� ������ �������
            playerStats.DecreasePower(damagePerSecond);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }

        // ��������������� �������� ������
        playerController.stopUpward  = false;

        // ������� ������� �����
        activeDebuff = null;

        // ���������� ������ ���� (�����)
        Destroy(gameObject);
    }
}
