using UnityEngine;
using System.Collections;

public class AbilityPickup : MonoBehaviour
{
    private BoxCollider2D boxCollider; // ������ �� ���������

    private void Start()
    {
        // ������� ��������� � ��������� ��� �� ��������������
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        // �������� ������� ��� ��������� ���������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                CheckTouch(touchPosition);
            }
        }

        // �������� ������� ��� �� (���� ����)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CheckTouch(mousePosition);
        }
    }

    private void CheckTouch(Vector3 position)
    {
        Vector2 touchPos2D = new Vector2(position.x, position.y);

        // �������� ������� �� ���������� ��������, ���� �� �������
        if (boxCollider != null && boxCollider.OverlapPoint(touchPos2D))
        {
            OnPickup();
        }
    }

    private void OnPickup()
    {
        // ���������� ��������� ������
        var weaponUpgrade = GetComponentInChildren<WeaponUpgradeBonus>();
        if (weaponUpgrade != null)
        {
            weaponUpgrade.ApplyUpgrade();
        }
        else
        {
            var levelBonus = GetComponentInChildren<WeaponLevelBonus>();
            if (levelBonus != null) levelBonus.ApplyUpgrade();
        }

        // ���������� connectedDoor
        var doorController = GetComponent<DoorController>();
        if (doorController != null && doorController.connectedDoor != null)
        {
            doorController.connectedDoor.SetActive(false);
        }
        else
        {
            Debug.LogWarning("DoorController ��� connectedDoor �� ������!");
        }

        // ������ �������� � ��������� ��� ��������� ������ ������
        StartCoroutine(ActivateWinScreenWithDelay());

        // �������� ������� ����� ��������������
        //Destroy(gameObject);
    }

    private IEnumerator ActivateWinScreenWithDelay()
    {
        // �������� � 1 �������
        yield return new WaitForSeconds(1f);

        // ��������� ������ ������
        var winScreenObject = GameObject.FindGameObjectWithTag("Win Screen");
        if (winScreenObject != null)
        {
            var winScreen = winScreenObject.GetComponent<WinActivate>();
            if (winScreen != null)
            {
                winScreen.WinScreenActivate();
            }
            else
            {
                Debug.LogWarning("WinActivate �� ������ �� ������� Win Screen!");
            }
        }
        else
        {
            Debug.LogWarning("������ � ����� 'Win Screen' �� ������!");
        }
    }
}
