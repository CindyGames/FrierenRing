using System.Collections;
using UnityEngine;

public class BackgroundFadeAndAnimate : MonoBehaviour
{
    // ������ �� SpriteRenderer, ������� ���������� ���
    private SpriteRenderer backgroundRenderer;

    // ������ �� �������� ��� ������� ��������
    public Animator animator;

    // �����, �� ������� ������������ ������������� �� ���������
    public float fadeDuration = 2f;

    // ������� �����-������������ (�� 0 �� 1)
    public float targetAlpha = 1f;

    // ����� �������� ����� ������� �������� ����� ���������� �������� ���������� ������������
    public float animationStartDelay = 1f;

    void Start()
    {
        // �������� ��������� SpriteRenderer
        backgroundRenderer = GetComponent<SpriteRenderer>();

    }

    public void StartDarkness()
    {
        if (backgroundRenderer != null)
        {
            // ��������� �������� ��� ���������� ������������
            StartCoroutine(FadeInBackground());
        }
    }

    // ��������� ��� �������� ���������� ������������
    private IEnumerator FadeInBackground()
    {
        float currentAlpha = backgroundRenderer.color.a;  // ������� �����-������������
        float elapsedTime = 0f;

        Color color = backgroundRenderer.color;

        // ���� ����� ������ ����������������� �������� ���������, ����������� �����
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            backgroundRenderer.color = color;
            yield return null;
        }

        // ������������� �������� �������� ������������
        color.a = targetAlpha;
        backgroundRenderer.color = color;

        // ���� ����� �������� ��������
        yield return new WaitForSeconds(animationStartDelay);

        // ��������� ��������, ���� �������� ��������
        if (animator != null)
        {
            animator.SetTrigger("StartAnimation");
        }
    }
}
