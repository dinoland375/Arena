using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float chanceIncreaseFactor = 0.1f;
    [SerializeField] private float lowHealthThreshold = 0.3f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject textCountDeadEnemyPrefab;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button ultaButton;
    [SerializeField] private Image ultaButtonLine;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform parentTextCountPrefab;
    [SerializeField] private float chanceToRicochet = 0.2f;
    [SerializeField] private AudioClip[] soundEffect;

    public float currentChanceToRicochet;
    public int deadEnemyCount;

    private AudioSource audioSource;
    private EnemySpawner enemiesList;
    private bool ultaAttackActive;

    private void Start()
    {
        enemiesList = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        attackButton.onClick.AddListener(Attack);
        ultaButton.onClick.AddListener(UltaAttack);
        currentChanceToRicochet = chanceToRicochet;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (ultaButtonLine.fillAmount >= 1)
            ultaAttackActive = true;
        else
            ultaAttackActive = false;
    }

    private void Attack()
    {
        audioSource.PlayOneShot(soundEffect[0]);
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Destroy(bullet, 5f);
    }
    
    private void UltaAttack()
    {
        if (ultaAttackActive)
        {
            ultaButtonLine.fillAmount = 0f;
            currentChanceToRicochet = 0.1f;

            foreach (GameObject enemy in enemiesList.enemies)
            {
                Destroy(enemy);
                deadEnemyCount++;
                GameObject count = Instantiate(textCountDeadEnemyPrefab, parentTextCountPrefab);
                Destroy(count, 1f);
            }
            enemiesList.enemies.Clear();

            GetComponent<PlayerHealth>().Heal(GetComponent<PlayerHealth>().maxHealth);
        }
    }

    public void KillEnemy(Collider enemy)
    {
        audioSource.PlayOneShot(soundEffect[1]);

        ultaButtonLine.fillAmount += 0.1f;
        deadEnemyCount++;

        GameObject count = Instantiate(textCountDeadEnemyPrefab, parentTextCountPrefab);

        Destroy(count, 1f);
        Destroy(enemy.gameObject);

        if (currentChanceToRicochet < 1f)
        {
            currentChanceToRicochet += chanceIncreaseFactor;
        }
    }

    public void OnHealthChanged()
    {
        if (GetComponent<PlayerHealth>().healthProgress  <= lowHealthThreshold)
        {
            currentChanceToRicochet = 1f;
        }
    }
}