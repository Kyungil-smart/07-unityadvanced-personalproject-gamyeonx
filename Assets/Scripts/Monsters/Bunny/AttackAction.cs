using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Self] [Target] [AttackRange] [DashSpeed] [JumpForce]", category: "Action", id: "5920af41a2831e0c1e0683fc65f4b5e2")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<float> DashSpeed;
    [SerializeReference] public BlackboardVariable<float> JumpForce;

    private bool _initialized = false;
    private Rigidbody2D _rb;

    protected override Status OnStart()
    {
        if (!_initialized)
        {
            _rb = Self.Value.GetComponent<Rigidbody2D>();
            _initialized = true;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);

        if (distance <= AttackRange.Value)
        {

            return Status.Success; 
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

