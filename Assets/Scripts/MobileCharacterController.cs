using UnityEngine;

public class MobileCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �����������
    public float upwardSpeed = 3f; // �������� ����������� �������� �����
    public float screenBoundaryPadding = 0.1f; // ������ �� ������ ������ ��� ����������� ��������
    public bool stopMove = false;
    public bool stopUpward = false;

    private Vector2 screenBounds;

    void Start()
    {
        // �������� ������� ������
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (!stopMove)
        {
            // �������� ��������� �����
            MoveUpward();

            if (!stopUpward)
            {
                // ���������� ���������� �� �����������
                HandleTouchMovement(); // ���������� ����� ������� ������
                HandleKeyboardMovement(); // ���������� � ����������
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

            // ��������� ������ �������
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // ��������� ������� ������� � ������� ����������
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));

                // ������� ��������� ������ � ����� ������� �� �����������
                Vector3 targetPosition = new Vector3(touchPosition.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }

        // ������������ �������� � �������� ������
        ClampPosition();
    }

    void HandleKeyboardMovement()
    {
        // ��������� ������� ������ "A" ��� ������� ����� ��� �������� �����
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        // ��������� ������� ������ "D" ��� ������� ������ ��� �������� ������
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // ������������ �������� � �������� ������
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
        // ������������ ��������� �� �����������
        float clampedX = Mathf.Clamp(transform.position.x, screenBounds.x * -1 + screenBoundaryPadding, screenBounds.x - screenBoundaryPadding);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
