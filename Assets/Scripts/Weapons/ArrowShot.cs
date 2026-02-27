using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    [SerializeField] private int _arrowPower = 10;
    [SerializeField] private float _arrowPowerY = 5f;
    [SerializeField] private Rigidbody2D _rigidbody;
    public int Attack { get; set; }



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.6f);
        _rigidbody.linearVelocity = new Vector2(transform.right.x * _arrowPower, _arrowPowerY);
    }
}
