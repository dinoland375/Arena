using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private int maxBounces = 1;

    private Vector3 initialPosition;
    private float distanceTravelled;
    private PlayerAttack player;
    private int bounceCount = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        distanceTravelled = Vector3.Distance(transform.position, initialPosition);

        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.KillEnemy(other.gameObject.GetComponent<Collider>());
            
            if (bounceCount < maxBounces && (Random.value < player.currentChanceToRicochet || player.currentChanceToRicochet == 1))
            {
                var nearbyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (nearbyEnemies.Length > 0)
                {
                    var closestEnemy = nearbyEnemies[0];

                    foreach (var enemy in nearbyEnemies)
                    {
                        if (enemy == other.gameObject) continue;

                        if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
                        {
                            closestEnemy = enemy;
                        }
                    }

                    var direction = (closestEnemy.transform.position - transform.position).normalized;
                    transform.forward = direction;
                    bounceCount++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}