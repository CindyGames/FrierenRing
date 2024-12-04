using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // Ссылка на объект игрока
    public float yOffset = 3f; // Смещение камеры по оси Y, чтобы персонаж был внизу экрана

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Перемещаем камеру только если игрок сдвинулся достаточно далеко
        float targetY = player.position.y + yOffset;
        if (Mathf.Abs(transform.position.y - targetY) > 0.01f) // Допустим, 0.01f — допустимая погрешность
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
}
