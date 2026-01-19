using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GunBounceController : MonoBehaviour
{
    [SerializeField] private float bouncePower = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall"))
            return;

        Vector2 hitPoint = other.ClosestPoint(rb.position);
        Vector2 normal = (rb.position - hitPoint).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.AddForce(normal * bouncePower, ForceMode2D.Impulse);
    }
}
