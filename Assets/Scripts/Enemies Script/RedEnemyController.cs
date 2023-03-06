using UnityEngine;

public class RedEnemyController : MonoBehaviour
{
    [SerializeField] private float upSpeed = 2f;
    [SerializeField] private float freezeTime = 10f;
    [SerializeField] private float attackSpeed = 10f;
    [SerializeField] private int damage = 15;

    private Transform player;
    private float freezeTimer = 0f;
    private bool isAttacking;
    private bool isFrozen;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isFrozen && !isAttacking)
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }

        if (transform.position.y > player.position.y + 5f && !isFrozen && !isAttacking)
        {
            isFrozen = true;
            freezeTimer = freezeTime;
        }

        if (isFrozen && freezeTimer > 0f)
        {
            freezeTimer -= Time.deltaTime;
        }
        else if (isFrozen && freezeTimer <= 0f)
        {
            isFrozen = false;
            isAttacking = true;
        }

        if (isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, attackSpeed * Time.deltaTime);

            if (transform.position == player.position)
            {
                Handheld.Vibrate();
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
