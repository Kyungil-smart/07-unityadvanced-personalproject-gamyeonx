using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Self] [Target] [AttackRange]", category: "Action", id: "5920af41a2831e0c1e0683fc65f4b5e2")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> AttackRange;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);

        if (distance <= AttackRange.Value)
        {
            Debug.Log("어택!!");
            return Status.Success;
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

