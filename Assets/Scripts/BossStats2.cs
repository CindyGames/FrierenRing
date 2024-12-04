using System.Collections;
using TMPro;
using UnityEngine;

public class BossStats2 : MonoBehaviour
{
    [SerializeField]
    int minHealth, maxHealth;  // �������� �������� �����

    private int health; // ������� �������� �����


    public TextMeshProUGUI healthText; // ��������� UI ������� ��� ����������� ��������
    public GameObject victoryScreen; // ����� ������

    private bool raige = false;
    public AudioSource bossMusic; // ������ �� AudioSource ��� ������� ������
    public AudioSource hitMusic; // ������ �� AudioSource ��� ������� ������

    private void Start()
    {
        if (victoryScreen == null)
        {
            victoryScreen = GameObject.FindGameObjectWithTag("Win Screen").GetComponent<WinActivate>().winScreen;
        }

        victoryScreen.SetActive(false);

        //bossMusic = GetComponent<AudioSource>();

        healthText = GetComponentInChildren<TextMeshProUGUI>();
        // ������ ��������� ��������� �������� � ��������� ���������
        health = Random.Range(minHealth, maxHealth);
        UpdateHealthUI();
    }

    // ��������� UI � ������� ��������� �����
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    // ����� ��� ��������� ����� �� ��������
    public void TakeDamage(int damageAmount)
    {
        hitMusic.Play();

        health -= damageAmount;
        UpdateHealthUI();
        if (health < maxHealth / 2 && raige == false)
        {
            raige = true;
            GetComponent<BossAttack>().attackInterval /= 2;
        }
        // ���� �������� �������� 0 ��� ����, ���� ��������
        if (health <= 0)
        {
            DefeatBoss();
        }
    }



    // ����� ��� ������ ��� ������
    private void DefeatBoss()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MobileCharacterController>().stopMove = true;
        //player.GetComponent<PlayerShooting>().stopShoot = true;
        //player.GetComponent<Collider2D>().enabled = false;

        //GetComponent<BossReward>().OnBossDefeated();

        // ���������� ����� ������
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }

        GetComponent<BossAttack>().attackInterval = 99; //stop attack

        if (bossMusic != null)
        {
            StartCoroutine(FadeOutMusic());
        }
        // ���������� ������ �����
        Destroy(gameObject, 4f);
    }


    // ����������� ��� �������� �������� ��������� ������
    private IEnumerator FadeOutMusic()
    {
        float startVolume = bossMusic.volume;
        float fadeDuration = 4f; // ����������������� ��������� ������

        // ������� ���������� ���������
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bossMusic.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        // ������������� ��������� �� 0 � ������������� ������
        bossMusic.volume = 0;
        bossMusic.Stop();
    }
}
