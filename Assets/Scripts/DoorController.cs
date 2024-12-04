using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject connectedDoor; // Ссылка на связанную дверь

    private bool isActive = true; // Флаг, активна ли дверь

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            if (connectedDoor != null)
            {
                // Отключаем связанную дверь
                connectedDoor.SetActive(false);
            }


            // Можно отключить текущую дверь, если необходимо
            // gameObject.SetActive(false);
        }
    }

    public void ActivateDoor()
    {
        // Включаем дверь
        gameObject.SetActive(true);
        isActive = true;
    }

    public void DeactivateDoor()
    {
        // Отключаем дверь
        gameObject.SetActive(false);
        isActive = false;
    }
}
