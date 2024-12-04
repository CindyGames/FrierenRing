using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserDuration = 2f; // ����� ������������� ������
    public int laserDamage = 10; // ����, ������� ������� �����
    public Vector3 laserStartScale = new Vector3(1f, 0f, 1f); // ��������� ������� ������ (���� �� Y)
    public Vector3 laserEndScale = new Vector3(1f, 5f, 1f); // �������� ������� ������
    public float growSpeed = 5f; // �������� ����� ������
    public float damageInterval = 0.5f; // �������� ����� ���������� �����
    private SpriteRenderer spriteRenderer; // ������ �� ��������� SpriteRenderer
    private bool isPlayerInLaser = false; // ����, ��� ����� � ���� ������
    private CharacterStats playerStats; // ������ �� ��������� ������

    void Start()
    {
        // �������� ��������� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ������������� ������ ��������� ������ (���� �� Y)
        transform.localScale = laserStartScale;

        // ��������� ������ ��� ����������� ������ ����� ��������� ������
        Invoke("DestroyLaser", laserDuration);
    }

    void Update()
    {
        // ���������� ����������� ������ ������ �� ������� ��������
        if (transform.localScale.y < laserEndScale.y)
        {
            float newScaleY = Mathf.MoveTowards(transform.localScale.y, laserEndScale.y, growSpeed * Time.deltaTime);
            transform.localScale = new Vector3(laserEndScale.x, newScaleY, laserEndScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �������� ������ �������� ������
            playerStats = other.GetComponent<CharacterStats>();

            if (playerStats != null && !isPlayerInLaser)
            {
                // ������������� ����, ��� ����� � ���� �������� ������
                isPlayerInLaser = true;

                // ��������� �������� ��� �������������� ��������� �����
                StartCoroutine(DealDamageOverTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ����� ����� �������� ���� �������� ������, ������������� ��������� �����
            if (playerStats != null)
            {
                isPlayerInLaser = false;
                StopCoroutine(DealDamageOverTime());
            }
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (isPlayerInLaser)
        {
            if (playerStats != null)
            {
                // ������� ���� ������
                playerStats.DecreasePower(laserDamage);
            }

            // ���� �������� ����� ��������� ���������� �����
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // ���������� ����� ����� �����
    private void DestroyLaser()
    {
        Destroy(gameObject);
    }
}
