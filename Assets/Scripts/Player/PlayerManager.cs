using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private GameObject retryUI;

    [SerializeField] private GameObject _player;

    [SerializeField] private int _maxHp;
    private int _hp;
    [SerializeField] private int _maxMp;
    private int _mp;

    private int _exp;
    [SerializeField] private int _maxExp = 100;
    private int _level;

    public int MaxHP
    {
        get => _maxHp;
        set => _maxHp = Mathf.Max(value, 1);
    }

    public int HP
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, 0, _maxHp);

            if (_hp <= 0)
                Die();
        }
    }

    public int MaxMP
    {
        get => _maxMp;
        set => _maxMp = Mathf.Max(value, 1);
    }

    public int MP
    {
        get => _mp;
        set => _mp = Mathf.Clamp(value, 0, _maxMp);
    }

    public int EXP
    {
        get => _exp;
        set
        {
            _exp = value;
            if (_exp >= _maxExp)
            {
                _exp -= _maxExp;
                LevelUp();
            }
        }
    }

    [SerializeField] private int _attack;
    public int Attack
    {
        get => _attack;
        set => _attack = value;
    }

    private bool _invincibility = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        HP = _maxHp;
        UI_Manager.Instance.UpdatePlayerHP(HP, MaxHP);
    }

    public void GetExE(int exe)
    {
        EXP += exe;
    }

    private void LevelUp()
    {
        _level++;
        _maxExp = _level * 100;

        MaxHP += 20;
        HP = MaxHP;
        Attack += 1;

        UI_Manager.Instance.UpdatePlayerHP(HP, MaxHP);
    }

    public void TakeDamege(int damage)
    {
        if (_invincibility) return;

        HP -= damage;
        UI_Manager.Instance.UpdatePlayerHP(HP, MaxHP);

        if (HP > 0)
            StartCoroutine(InvincibilityCoroutine());
        else
            Die();
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _invincibility = true;
        yield return new WaitForSeconds(1f);
        _invincibility = false;
    }

    private void Die()
    {
        Destroy(_player);
        retryUI.SetActive(true);
    }
}
