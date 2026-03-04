using UnityEngine;

public class forMiniBunny : MonoBehaviour
{
    private int _groundLayer;
    private int _playerLayer;
    private int _addScore = 20;
    [SerializeField] private HitFlash _hitFlash;

    private void Awake()
    {
        _groundLayer = LayerMask.NameToLayer("Ground");
        _playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == _playerLayer)
        {
            MiniBunnySpawn.Instance.Death();
            _hitFlash?.hitFlash();

            Destroy(gameObject, 0.1f);
        }
        else if (layer == _groundLayer)
        {
            MiniBunnySpawn.Instance.AddScore(_addScore);
            Destroy(gameObject);
        }
    }
}
