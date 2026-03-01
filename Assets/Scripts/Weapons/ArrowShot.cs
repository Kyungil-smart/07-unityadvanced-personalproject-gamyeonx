using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    [SerializeField] private int _arrowPower = 10;
    [SerializeField] private float _arrowPowerY = 5f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _attack = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.6f);
        _rigidbody.linearVelocity = new Vector2(transform.right.x * _arrowPower, _arrowPowerY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Monster"))
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamege(_attack);
            Destroy(gameObject);
        }
    }
}
