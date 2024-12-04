using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Переключатели для управления масштабом по X и Y
    public bool scaleY = false;  // Если true, масштабируется и по оси Y

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleBackground();
    }

    public void ScaleBackground()
    {
        // Получаем размеры изображения
        float imageWidth = spriteRenderer.sprite.bounds.size.x;
        float imageHeight = spriteRenderer.sprite.bounds.size.y;

        // Получаем размеры экрана в мировых координатах
        float worldScreenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        float worldScreenHeight = Camera.main.orthographicSize * 2f;

        // Масштабируем по оси X (ширина) в любом случае
        Vector3 newScale = transform.localScale;
        newScale.x = worldScreenWidth / imageWidth;

        // Если флаг scaleY установлен, масштабируем и по оси Y (высота)
        if (scaleY)
        {
            newScale.y = worldScreenHeight / imageHeight;
        }

        // Применяем новый масштаб
        transform.localScale = newScale;
    }
}
