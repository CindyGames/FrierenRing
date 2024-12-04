using System.Collections;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour
{
    public GameObject boss;           // ������ �� ������ �����
    public float followDelay = 2f;    // �������� ����� ���, ��� ���� ������ ��������� �� �������
    public Animator bossAnimator;     // �������� ��� ���������� ���������� �����
    public float bossYOffset = 5f;    // ���������� �� ��� Y �� ������, ��� ������ ���������� ����
    public AudioSource bossMusic;     // ������ �� ��������� AudioSource ��� ������� ������
    public AudioSource backMusic;     // ������ �� ��������� AudioSource ��� ������� ������
    public LaserShooter laserShooter;
    public float triggerDistance = 5f; // ����������, ��� ������� ���� �������� ��������� �� �������
    public bool backraundBoss = false;
    public float moveSpeed = 2f;

    private bool isFollowing = false, move = false; // ��������, ����� �� ���� ��������� �� �������
    private Transform playerTransform; // ������ �� ��������� ������
    private MobileCharacterController controller;
    private BackgroundFadeAndAnimate backgroundFadeAndAnimate;
    private GameObject player;        // �����

    private Vector3 currentVelocity = Vector3.zero;  // ������ ������� �������� ��� SmoothDamp
    private void Start()
    {
        // ����� ������
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        // ������� ��������� �����������
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
            // �������� ���������� �� ������
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= triggerDistance)
            {
                isFollowing = true; // ��������� ����, ����� ��������� ���� ���
                StartCoroutine(BeginBossFollow()); // ��������� �������� ������ �������� �����
                //player.GetComponent<PlayerShooting>().stopShoot = true;

                // ������������� ������, ���� ��� ��� ���������
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
            // ���� ������� �� �������, �������� ���� ������ �� �������� ���������� �� ��� Y
            Vector3 bossTargetPosition = new Vector3(0, playerTransform.position.y + bossYOffset, playerTransform.position.z);

            boss.transform.position = Vector3.Lerp(boss.transform.position, bossTargetPosition, Time.deltaTime * moveSpeed); // ������� �������� �����

        }

    }

    // ����������� �������� ������
    IEnumerator PreloadMusic()
    {
        bossMusic.clip.LoadAudioData();  // ��������� �����������
        while (!bossMusic.clip.loadState.Equals(AudioDataLoadState.Loaded))
        {
            yield return null;  // ����, ���� �������� ����������
        }

    }

    // ����������� ��� �������� ����� ������� �������� �����
    IEnumerator BeginBossFollow()
    {


        // ������������� �������� ������
        controller = player.GetComponent<MobileCharacterController>();
        controller.stopMove = true;



        // �������� �������� ����� ��������� �����
        yield return new WaitForSeconds(followDelay);

        GetComponent<BoxCollider2D>().enabled = true;

        move = true;
        //player.GetComponent<PlayerShooting>().stopShoot = false;

        // �������� �������� �������� �����
        if (bossAnimator != null)
        {
            bossAnimator.SetBool("Move", true);
        }

        // ���� �������� ��������� �� �������
        GetComponent<BossAttack>().StartAttack();
        controller.stopMove = false;

        if (laserShooter != null)
        {
            laserShooter.startAttack = true;
        }

        if (backraundBoss)
        {
            // ����� ������ ���� � ��������� ���������� ����� 10 ������
            GameObject back = GameObject.FindGameObjectWithTag("Back");
            backgroundFadeAndAnimate = back.GetComponent<BackgroundFadeAndAnimate>();

            yield return new WaitForSeconds(33);
            backgroundFadeAndAnimate.StartDarkness();
        }

    }

    IEnumerator StopBackMusic()
    {
        float startVolume = backMusic.volume;
        float fadeDuration = 4f; // ����������������� ��������� ������

        // ������� ���������� ���������
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            backMusic.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        // ������������� ��������� �� 0 � ������������� ������
        backMusic.volume = 0;
        backMusic.Stop();
    }

}
