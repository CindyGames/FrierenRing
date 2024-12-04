using System.Collections;
using UnityEngine;
using System.Linq; // ��� ������������� LINQ �������

public class BackgroundManager : MonoBehaviour
{
    // ������ ���� ����� (��������)
    public Sprite[] backgrounds;

    // ������ �� SpriteRenderer, ������� ���������� ������� ���
    private SpriteRenderer backgroundRenderer;

    // ����� �������� ����� ����������
    //public float activationDelay = 2f;

    BackgroundScaler backgroundScaler;

    void Start()
    {
        backgroundScaler = GetComponent<BackgroundScaler>();
        // �������� ��������� SpriteRenderer �� �������
        backgroundRenderer = GetComponent<SpriteRenderer>();

        // ������� ������ �� null-���������
        CleanBackgroundList();
    }

    // ����� ��� ������� ������ �� null-���������
    void CleanBackgroundList()
    {
        // ��������� ������, �������� ������ �������� �������
        backgrounds = backgrounds.Where(sprite => sprite != null).ToArray();

        if (backgrounds.Length == 0)
        {
            Debug.LogWarning("No valid backgrounds available after cleaning!");
        }
    }

    // ����� ��� ����� �������� ���� �� ���������
    public void SetRandomBackground()
    {
        if (backgrounds.Length == 0)
        {
            Debug.LogWarning("No backgrounds available!");
            return;
        }

        // �������� ��������� ���
        int randomIndex = Random.Range(0, backgrounds.Length);

        // ������������� ��������� ���
        backgroundRenderer.sprite = backgrounds[randomIndex];
        backgroundScaler.ScaleBackground();
    }
}
