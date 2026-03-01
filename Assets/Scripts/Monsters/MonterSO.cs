using UnityEngine;

public class MonterSO : MonoBehaviour, IDamageable
{
    public MonsterTypeSO _type;

    [SerializeField] private int _hp;
    [SerializeField] private int _atk;
    [SerializeField] private int _expReward;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _hp = _type._hp;
        _atk = _type._atk;
        _expReward = _type._expReward;
    }

    public void TakeDamege(int amount)
    {
        _hp -= amount;

        if (_hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
