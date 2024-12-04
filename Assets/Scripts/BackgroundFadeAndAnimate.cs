using System.Collections;
using UnityEngine;

public class BackgroundFadeAndAnimate : MonoBehaviour
{
    // Ссылка на SpriteRenderer, который отображает фон
    private SpriteRenderer backgroundRenderer;

    // Ссылка на аниматор для запуска анимации
    public Animator animator;

    // Время, за которое прозрачность увеличивается до максимума
    public float fadeDuration = 2f;

    // Целевая альфа-прозрачность (от 0 до 1)
    public float targetAlpha = 1f;

    // Время задержки перед началом анимации после завершения плавного увеличения прозрачности
    public float animationStartDelay = 1f;

    void Start()
    {
        // Получаем компонент SpriteRenderer
        backgroundRenderer = GetComponent<SpriteRenderer>();

    }

    public void StartDarkness()
    {
        if (backgroundRenderer != null)
        {
            // Запускаем корутину для увеличения прозрачности
            StartCoroutine(FadeInBackground());
        }
    }

    // Короутина для плавного увеличения прозрачности
    private IEnumerator FadeInBackground()
    {
        float currentAlpha = backgroundRenderer.color.a;  // Текущая альфа-прозрачность
        float elapsedTime = 0f;

        Color color = backgroundRenderer.color;

        // Пока время меньше продолжительности плавного изменения, увеличиваем альфу
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);
            color.a = newAlpha;
            backgroundRenderer.color = color;
            yield return null;
        }

        // Устанавливаем конечное значение прозрачности
        color.a = targetAlpha;
        backgroundRenderer.color = color;

        // Ждем перед запуском анимации
        yield return new WaitForSeconds(animationStartDelay);

        // Запускаем анимацию, если аниматор доступен
        if (animator != null)
        {
            animator.SetTrigger("StartAnimation");
        }
    }
}
