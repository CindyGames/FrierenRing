using UnityEngine;
using System.Collections;

public class BearTrapMagnit : MonoBehaviour
{
    public float pullSpeed = 2f; // �������� ������������
    public float pullDuration = 2f; // �����, � ������� �������� ����� ����� ������������� � �������

    private bool isPullingPlayer = false; // ����, ����� ����������, ���������� �� ������������
    private Transform playerTransform; // ������ �� ��������� ������
    private Vector3 trapCenter; // ����� �������

    private void Start()
    {
        Destroy(gameObject, 30);
    }

    // ����� ����� ��������� �� �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPullingPlayer)
        {
            playerTransform = other.transform;
            trapCenter = transform.position; // ����� ������� � ��� ��� ������� �������
            isPullingPlayer = true;

            // ��������� �������� � �������� ��� ������������ ������
            StartCoroutine(PullPlayerToTrap());
        }
    }

    // �������� ��� �������� ������������ ������ � ������ ������� �� ��� X � ��������
    private IEnumerator PullPlayerToTrap()
    {
        float elapsedTime = 0f; // ������� �������

        while (elapsedTime < pullDuration)
        {
            // ������� ����������� ������ �� ��� X, �� ������� Y
            float newX = Mathf.Lerp(playerTransform.position.x, trapCenter.x, pullSpeed * Time.deltaTime);
            playerTransform.position = new Vector3(newX, playerTransform.position.y, playerTransform.position.z);

            // ����������� ����� �� ������ �����
            elapsedTime += Time.deltaTime;

            // ���� ��������� ����
            yield return null;
        }

        // ������������� ������������ �� ��������� �������
        isPullingPlayer = false;
    }
}
