using UnityEngine;

public class MobileCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения
    public float upwardSpeed = 3f; // Скорость постоянного движения вверх
    public float screenBoundaryPadding = 0.1f; // Отступ от границ экрана для ограничения движения
    public bool stopMove = false;
    public bool stopUpward = false;

    private Vector2 screenBounds;

    void Start()
    {
        // Получаем размеры экрана
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (!stopMove)
        {
            // Движение персонажа вверх
            MoveUpward();

            if (!stopUpward)
            {
                // Управление персонажем по горизонтали
                HandleTouchMovement(); // Управление через касание экрана
                HandleKeyboardMovement(); // Управление с клавиатуры
            }

        }
    }

    void MoveUpward()
    {
        transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime);
    }

    void HandleTouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Проверяем стадию касания
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // Переводим позицию касания в мировые координаты
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));

                // Двигаем персонажа плавно к точке касания по горизонтали
                Vector3 targetPosition = new Vector3(touchPosition.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }

        // Ограничиваем движение в пределах экрана
        ClampPosition();
    }

    void HandleKeyboardMovement()
    {
        // Проверяем нажатие клавиш "A" или стрелка влево для движения влево
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        // Проверяем нажатие клавиш "D" или стрелка вправо для движения вправо
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // Ограничиваем движение в пределах экрана
        ClampPosition();
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void ClampPosition()
    {
        // Ограничиваем персонажа по горизонтали
        float clampedX = Mathf.Clamp(transform.position.x, screenBounds.x * -1 + screenBoundaryPadding, screenBounds.x - screenBoundaryPadding);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
