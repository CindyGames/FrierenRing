using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject backgroundPrefab; // ������ ���� ��� �������� �����

    private float backgroundHeight; // ������ ������ ����
    private Transform playerTransform; // ������ �� ������ ��� ������
    private GameObject[] backgrounds; // ������ ��� �������� �����
    private int currentBackgroundIndex = 0; // ������ �������� ����

    void Start()
    {
        // ��������� ������� ������� ����
        if (backgroundPrefab == null)
        {
            Debug.LogError("backgroundPrefab �� �������� � ����������.");
            return; // ��������� ����������, ���� ������ �� ��������
        }

        // �������� ������ �� ������ (��� ������)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("����� � ����� 'Player' �� ������.");
            return; // ��������� ����������, ���� ����� �� ������
        }

        // ���������� ������ ���� �������������
        backgroundHeight = GetBackgroundHeight();
        if (backgroundHeight == 0f)
        {
            Debug.LogError("�� ������� ���������� ������ ����. ��������� ������� SpriteRenderer �� backgroundPrefab.");
            return; // ��������� ����������, ���� ������ �� ����������
        }

        // ������� ������ ����� � ��������� ��
        backgrounds = new GameObject[4]; // ����� ������ ����: �������� � ��� �����
        backgrounds[0] = this.gameObject; // ������� ���

        // ������� ��� ����� ����
        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (backgroundPrefab != null)
            {
                // ������� ����� �������
                GameObject newBackground = Instantiate(backgroundPrefab, transform.position + new Vector3(0, backgroundHeight * i, 0), Quaternion.identity);

                // ������� ������ InfiniteBackground � �����
                InfiniteBackground script = newBackground.GetComponent<InfiniteBackground>();
                if (script != null)
                {
                    Destroy(script);
                }

                // ��������� ����� � ������ �����
                backgrounds[i] = newBackground;
            }
        }


    }

    void Update()
    {
        if (playerTransform == null) return; // �������� �� ������� ������

        // ���������, ���� ������� ��� ��������� ���� ������ (������)
        if (playerTransform.position.y - backgrounds[currentBackgroundIndex].transform.position.y > backgroundHeight)
        {
            // ���������� ���, ������� ��������� ���� ����, ������ (�����)
            RepositionBackground();
        }
    }

    // ����� ��� ����������� ���� ������ (�����)
    private void RepositionBackground()
    {
        if (backgrounds.Length == 0) return; // �������� �� ������� ����� � �������

        // ������ ����, ������� ��������� ���� ���� (����� ���������)
        int farthestBackgroundIndex = (currentBackgroundIndex + backgrounds.Length - 1) % backgrounds.Length;

        // ���������� ������� ��� ������ �� ������ ���� �����, ����� �� �������� �� ����� ������� �����
        backgrounds[currentBackgroundIndex].transform.position = backgrounds[farthestBackgroundIndex].transform.position + new Vector3(0, backgroundHeight, 0);

        // ��������� ������ �������� ����
        currentBackgroundIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
    }

    // ����� ��� ��������������� ����������� ������ ����
    private float GetBackgroundHeight()
    {
        if (backgroundPrefab != null)
        {
            // ��������� �������� ��������� SpriteRenderer ��� ����������� ������ �������
            SpriteRenderer spriteRenderer = backgroundPrefab.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // ���� ���� SpriteRenderer, ���������� ������ ������� � ������� �����������
                return spriteRenderer.bounds.size.y;
            }
        }

        Debug.LogWarning("SpriteRenderer �� ������ �� backgroundPrefab! ���������, ��� �� ���� ���� ��������� SpriteRenderer.");
        return 0f;
    }

}
