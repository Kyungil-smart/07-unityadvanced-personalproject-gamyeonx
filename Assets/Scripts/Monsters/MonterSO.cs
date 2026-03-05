using UnityEngine;
using UnityEngine.SceneManagement;

public class MonterSO : MonoBehaviour, IDamageable
{
    public MonsterTypeSO _type;

    [SerializeField] private MonsterCategory _category;
    [SerializeField] private int _hp;
    [SerializeField] private int _maxhp;
    [SerializeField] private int _expReward;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _sfxClip;

    [SerializeField] private HitFlash _hitFlash;

    [SerializeField] private GameObject Item;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _category = _type._category;
        _hp = _type._maxhp;
        _expReward = _type._expReward;
    }

    public void TakeDamege(int amount)
    {
        _hitFlash?.hitFlash();
        _sfxSource.PlayOneShot(_sfxClip);

        _hp -= amount;

        if (_category == MonsterCategory.Boss)
            UI_Manager.Instance.UpdateBossHP(_hp, _type._hp);

        if (_hp <= 0)
        {
            PlayerManager.Instance.GetExP(_expReward);
            UI_Manager.Instance.UpdatePlayerEXP(PlayerManager.Instance.EXP, PlayerManager.Instance.MaxEXP);
            Die();
        }
    }

    private void Die()
    {
        if (_category == MonsterCategory.Boss)
        {
            Instantiate(Item, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
