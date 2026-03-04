using UnityEngine;

public enum MonsterCategory
{
    Normal,
    Boss
}

[CreateAssetMenu(menuName = "SO/Monser Type")]
public class MonsterTypeSO : ScriptableObject
{
    [Header("기본 정보")]
    public MonsterCategory _category;
    public string _name;
    public int _hp;
    public int _maxhp;

    [Header("SFX")]
    [SerializeField] public AudioClip _sfxClip;

    [Header("보상 관련")]

    public int _expReward;
}
