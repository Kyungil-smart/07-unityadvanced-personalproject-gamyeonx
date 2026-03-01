using UnityEngine;

[CreateAssetMenu(menuName = "SO/Monser Type")]
public class MonsterTypeSO : ScriptableObject
{
    [Header("기본 정보")]
    public int _id;
    public string _name;
    public int _hp;
    public int _atk;
    
    [Header("보상 관련")]

    public int _expReward;
}
