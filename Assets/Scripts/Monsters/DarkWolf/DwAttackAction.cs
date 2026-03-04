using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "dwAttack", story: "[Self] [Target] [AttackRange] [dwAnimator]", category: "Action", id: "0104c7df5cb8bc5ca47af6099f6e4f7d")]
public partial class DwAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<Animator> DwAnimator;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);

        if (distance <= AttackRange.Value)
        {
            DwAnimator.Value.SetTrigger("IsAttack");
            PlayerManager.Instance.TakeDamege(20);
            return Status.Success;
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

