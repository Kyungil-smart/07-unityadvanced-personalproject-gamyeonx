using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    [SerializeField] private int _arrowPower = 15;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.linearVelocity = transform.right * _arrowPower;
    }
}
