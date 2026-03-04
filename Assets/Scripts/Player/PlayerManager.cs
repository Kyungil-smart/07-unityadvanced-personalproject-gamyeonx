using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;

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
    private int _level = 1;

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

            while (_exp >= _maxExp)
            {
                _exp -= _maxExp;
                LevelUp();
            }
        }
    }

    public int MaxEXP
    {
        get => _maxExp;
        set => _maxExp = value;
    }

    [SerializeField] private int _attack;
    public int Attack
    {
        get => _attack;
        set => _attack = value;
    }

    private bool _invincibility = false;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip[] _sfxClip;

    [SerializeField] private GameObject _lvUp;

    [SerializeField] private HitFlash _hitFlash;

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
        MP = _maxMp;
        UI_Manager.Instance.UpdatePlayerHP(HP, MaxHP);
        UI_Manager.Instance.UpdatePlayerMP(MP, MaxMP);
        UI_Manager.Instance.UpdatePlayerEXP(EXP, MaxEXP);
        UI_Manager.Instance.UpdateLevel(_level);
    }

    public void GetExP(int exp)
    {
        EXP += exp;
    }

    private void LevelUp()
    {
        if (_level >= 99) return;

        Vector3 backOffset = transform.up * 1.333f;
        Instantiate(_lvUp, _player.transform.position + backOffset, _player.transform.rotation, _player.transform);
        _sfxSource.PlayOneShot(_sfxClip[1]);
        _level++;
        _maxExp = _level * 100;

        MaxHP += 25;
        HP = MaxHP;
        MaxMP += 25;
        MP = MaxMP;
        Attack += 1;

        UI_Manager.Instance.UpdatePlayerHP(HP, MaxHP);
        UI_Manager.Instance.UpdatePlayerMP(MP, MaxMP);
        UI_Manager.Instance.UpdatePlayerEXP(EXP, MaxEXP);
        UI_Manager.Instance.UpdateLevel(_level);
    }

    public void TakeDamege(int damage)
    {
        if (_invincibility) return;

        _hitFlash?.hitFlash();
        _sfxSource.PlayOneShot(_sfxClip[0]);
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
