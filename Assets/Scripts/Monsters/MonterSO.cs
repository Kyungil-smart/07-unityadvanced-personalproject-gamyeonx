using UnityEngine;

public class MonterSO : MonoBehaviour, IDamageable
{
    public MonsterTypeSO _type;

    [SerializeField] private int _hp;
    [SerializeField] private int _expReward;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _sfxClip;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _hp = _type._hp;
        _expReward = _type._expReward;
    }

    public void TakeDamege(int amount)
    {
        _sfxSource.PlayOneShot(_sfxClip);

        _hp -= amount;

        if (_hp <= 0)
        {
            PlayerManager.Instance.GetExP(_expReward);
            UI_Manager.Instance.UpdatePlayerEXP(PlayerManager.Instance.EXP, PlayerManager.Instance.MaxEXP);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
