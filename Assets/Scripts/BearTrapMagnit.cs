using UnityEngine;
using System.Collections;

public class BearTrapMagnit : MonoBehaviour
{
    public float pullSpeed = 2f; // —корость прит€гивани€
    public float pullDuration = 2f; // ¬рем€, в течение которого игрок будет прит€гиватьс€ к капкану

    private bool isPullingPlayer = false; // ‘лаг, чтобы определить, происходит ли прит€гивание
    private Transform playerTransform; // —сылка на трансформ игрока
    private Vector3 trapCenter; // ÷ентр капкана

    private void Start()
    {
        Destroy(gameObject, 30);
    }

    //  огда игрок наступает на ловушку
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPullingPlayer)
        {
            playerTransform = other.transform;
            trapCenter = transform.position; // ÷ентр капкана Ч это его текуща€ позици€
            isPullingPlayer = true;

            // «апускаем корутину с таймером дл€ прит€гивани€ игрока
            StartCoroutine(PullPlayerToTrap());
        }
    }

    //  орутина дл€ плавного прит€гивани€ игрока к центру капкана по оси X с таймером
    private IEnumerator PullPlayerToTrap()
    {
        float elapsedTime = 0f; // —чЄтчик времени

        while (elapsedTime < pullDuration)
        {
            // ѕлавное перемещение игрока по оси X, не измен€€ Y
            float newX = Mathf.Lerp(playerTransform.position.x, trapCenter.x, pullSpeed * Time.deltaTime);
            playerTransform.position = new Vector3(newX, playerTransform.position.y, playerTransform.position.z);

            // ”величиваем врем€ на каждом кадре
            elapsedTime += Time.deltaTime;

            // ∆дем следующий кадр
            yield return null;
        }

        // ќстанавливаем прит€гивание по истечении времени
        isPullingPlayer = false;
    }
}
