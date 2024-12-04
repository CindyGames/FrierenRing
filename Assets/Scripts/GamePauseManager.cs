using UnityEngine;
using UnityEngine.EventSystems; // ��� ������ � UI
using System.Collections.Generic; // ��� ������ � List

public class GamePauseManager : MonoBehaviour
{
    public bool isGamePaused = true;
    public MobileCharacterController characterController; // ������ �� ������ �������� ���������
    public List<GameObject> objectsToToggle; // ������ ��������, ������� ����� ��������/���������

    private Vector2 startTouchPosition;  // ��������� ������� �������
    private Vector2 endTouchPosition;    // �������� ������� �������
    private float minSwipeDistance = 15f; // ����������� ���������� ��� ������

    void Start()
    {
        // �������� ���� � �����
        PauseGame();
    }

    void Update()
    {
        if (isGamePaused)
        {
            // �������� ������ ��� ��������� ���������
            if (Input.touchCount > 0 && !IsPointerOverUI())
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                    CheckSwipe();
                }
            }

            // �������� ������� ������ ��� ��
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                ResumeGame();
            }
        }
    }

    // ����� ��� ����� ����
    public void PauseGame()
    {
        if (characterController != null)
        {
            characterController.stopMove = true; // ��������� �������� ���������
        }

        isGamePaused = true;

        // �������� UI ������� �� ������
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(true);
        }
    }

    // ����� ��� ����������� ����
    public void ResumeGame()
    {
        if (characterController != null)
        {
            characterController.stopMove = false; // �������� �������� ���������
        }

        isGamePaused = false;

        // ��������� UI ������� �� ������
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }
    }

    // ����� ��� �������� ������
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(endTouchPosition.x - startTouchPosition.x);

        if (swipeDistanceX > minSwipeDistance)
        {
            ResumeGame();
        }
    }

    // ����� ��� �������� ������� �� UI
    private bool IsPointerOverUI()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = touch.position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
        return false;
    }
}
