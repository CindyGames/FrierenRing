using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // ������������� ��� ���������� ��������� �� X � Y
    public bool scaleY = false;  // ���� true, �������������� � �� ��� Y

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleBackground();
    }

    public void ScaleBackground()
    {
        // �������� ������� �����������
        float imageWidth = spriteRenderer.sprite.bounds.size.x;
        float imageHeight = spriteRenderer.sprite.bounds.size.y;

        // �������� ������� ������ � ������� �����������
        float worldScreenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        float worldScreenHeight = Camera.main.orthographicSize * 2f;

        // ������������ �� ��� X (������) � ����� ������
        Vector3 newScale = transform.localScale;
        newScale.x = worldScreenWidth / imageWidth;

        // ���� ���� scaleY ����������, ������������ � �� ��� Y (������)
        if (scaleY)
        {
            newScale.y = worldScreenHeight / imageHeight;
        }

        // ��������� ����� �������
        transform.localScale = newScale;
    }
}
