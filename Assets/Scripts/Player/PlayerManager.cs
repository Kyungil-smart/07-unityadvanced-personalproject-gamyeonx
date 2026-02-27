using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

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
        set => _hp = Mathf.Clamp(value, 0, _maxHp);
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

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LevelUp()
    {
        _level++;
        _maxExp = _level * 100;
    }
}
