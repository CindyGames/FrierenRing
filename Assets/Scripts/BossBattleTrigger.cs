using System.Collections;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour
{
    public GameObject boss;           // Ссылка на объект босса
    public float followDelay = 2f;    // Задержка перед тем, как босс начнет двигаться за игроком
    public Animator bossAnimator;     // Аниматор для управления анимациями босса
    public float bossYOffset = 5f;    // Расстояние по оси Y от игрока, где должен находиться босс
    public AudioSource bossMusic;     // Ссылка на компонент AudioSource для фоновой музыки
    public AudioSource backMusic;     // Ссылка на компонент AudioSource для фоновой музыки
    public LaserShooter laserShooter;
    public float triggerDistance = 5f; // Расстояние, при котором босс начинает двигаться за игроком
    public bool backraundBoss = false;
    public float moveSpeed = 2f;

    private bool isFollowing = false, move = false; // Проверка, начал ли босс следовать за игроком
    private Transform playerTransform; // Ссылка на трансформ игрока
    private MobileCharacterController controller;
    private BackgroundFadeAndAnimate backgroundFadeAndAnimate;
    private GameObject player;        // Игрок

    private Vector3 currentVelocity = Vector3.zero;  // Хранит текущую скорость для SmoothDamp
    private void Start()
    {
        // Поиск игрока
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        // Заранее загружаем аудиоданные
        if (bossMusic != null && bossMusic.clip != null)
        {
            StartCoroutine(PreloadMusic());
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void FixedUpdate()
    {
        if (!isFollowing && playerTransform != null)
        {
            // Проверка расстояния до игрока
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= triggerDistance)
            {
                isFollowing = true; // Обновляем флаг, чтобы сработало один раз
                StartCoroutine(BeginBossFollow()); // Запускаем корутину начала движения босса
                //player.GetComponent<PlayerShooting>().stopShoot = true;

                // Воспроизводим музыку, если она уже загружена
                if (bossMusic != null && bossMusic.clip.loadState == AudioDataLoadState.Loaded && !bossMusic.isPlaying)
                {
                    bossMusic.Play();

                    if (backMusic != null)
                    {
                        StartCoroutine(StopBackMusic());
                    }

                }
            }
        }
        if (isFollowing && playerTransform != null && move == true)
        {
            // Босс следует за игроком, находясь выше игрока на заданное расстояние по оси Y
            Vector3 bossTargetPosition = new Vector3(0, playerTransform.position.y + bossYOffset, playerTransform.position.z);

            boss.transform.position = Vector3.Lerp(boss.transform.position, bossTargetPosition, Time.deltaTime * moveSpeed); // Плавное движение босса

        }

    }

    // Асинхронная загрузка музыки
    IEnumerator PreloadMusic()
    {
        bossMusic.clip.LoadAudioData();  // Загружаем аудиоданные
        while (!bossMusic.clip.loadState.Equals(AudioDataLoadState.Loaded))
        {
            yield return null;  // Ждем, пока загрузка завершится
        }

    }

    // Сопрограмма для задержки перед началом движения босса
    IEnumerator BeginBossFollow()
    {


        // Останавливаем движение игрока
        controller = player.GetComponent<MobileCharacterController>();
        controller.stopMove = true;



        // Ожидание задержки перед движением босса
        yield return new WaitForSeconds(followDelay);

        GetComponent<BoxCollider2D>().enabled = true;

        move = true;
        //player.GetComponent<PlayerShooting>().stopShoot = false;

        // Включаем анимацию движения босса
        if (bossAnimator != null)
        {
            bossAnimator.SetBool("Move", true);
        }

        // Босс начинает следовать за игроком
        GetComponent<BossAttack>().StartAttack();
        controller.stopMove = false;

        if (laserShooter != null)
        {
            laserShooter.startAttack = true;
        }

        if (backraundBoss)
        {
            // Найти объект фона и запустить затемнение через 10 секунд
            GameObject back = GameObject.FindGameObjectWithTag("Back");
            backgroundFadeAndAnimate = back.GetComponent<BackgroundFadeAndAnimate>();

            yield return new WaitForSeconds(33);
            backgroundFadeAndAnimate.StartDarkness();
        }

    }

    IEnumerator StopBackMusic()
    {
        float startVolume = backMusic.volume;
        float fadeDuration = 4f; // Продолжительность затухания музыки

        // Плавное уменьшение громкости
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            backMusic.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        // Устанавливаем громкость на 0 и останавливаем музыку
        backMusic.volume = 0;
        backMusic.Stop();
    }

}
