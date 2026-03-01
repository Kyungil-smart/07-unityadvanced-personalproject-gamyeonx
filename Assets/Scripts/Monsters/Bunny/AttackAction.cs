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

    private bool _initialized = false;
    private bool _attacked = false;

    protected override Status OnStart()
    {
        if (!_initialized)
        {
            _initialized = true;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_attacked)
            return Status.Success;

        float distance = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);

        if (distance <= AttackRange.Value)
        {
            PlayerManager.Instance.TakeDamege(10);
            _attacked = true;
            return Status.Success;
            }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
        _attacked = false;
    }
}

