using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] private int damageAmount = 10;

    public float bulletSpeed = 10.0f;

    private GameObject player;
    private Vector3 targetPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        targetPosition = player.transform.position + directionToPlayer * bulletSpeed;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Handheld.Vibrate();
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
