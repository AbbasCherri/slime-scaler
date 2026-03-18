using UnityEngine;

public class MovingPlat : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = pointB.position;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, target) < 0.05f)
        {
            target = target == (Vector2)pointA.position ? pointB.position : pointA.position;
        }
    }
}