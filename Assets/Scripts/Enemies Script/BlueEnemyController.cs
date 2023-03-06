using UnityEngine;

public class BlueEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletLifetime = 5.0f;
    [SerializeField] private float attackInterval = 5.0f;
    [SerializeField] private float teleportDistance = 10.0f;
    [SerializeField] private float speedEnemy = 10.0f;
    [SerializeField] private Vector3 directionToPlayer;

    public Vector3 targetPosition;
    public float bulletSpeed = 10.0f;
    public GameObject player;
    public Vector3 lastTargenPosition;

    private float lastAttackTime = 0.0f;
    private EnemySpawner enemySpawnerList;
    private GameObject bullet;

    private void Start()
    {
        enemySpawnerList = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = player.transform.position;
    }

    private void Update()
    {
        Vector3 directionRotate = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionRotate);

        if (bullet != null) 
        {
            if (targetPosition != null)
            {
                Vector3 direction = targetPosition - bullet.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        if (Time.time - lastAttackTime > attackInterval && bullet == null)
        {
            FireBullet();
            lastAttackTime = Time.time;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > teleportDistance)
        {
            targetPosition = player.transform.position;
        }

    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position);
        targetPosition = player.transform.position + directionToPlayer * speedEnemy;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedEnemy * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Destroy(bullet);
    }

    private void FireBullet()
    {
        if(bullet == null)
        {
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            enemySpawnerList.enemies.Add(bullet);
            Destroy(bullet, bulletLifetime);
        }
    }
}
