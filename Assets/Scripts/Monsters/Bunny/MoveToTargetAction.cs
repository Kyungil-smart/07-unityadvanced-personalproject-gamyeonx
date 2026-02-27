using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[Self] [Target] [DetectRange] [MoveSpeed]", category: "Action", id: "275b15c79c27d49dc4c4ba477b6ba560")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> DetectRange;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);


        if (distance <= DetectRange.Value)
        {
            Self.Value.transform.position = Vector2.MoveTowards(
            Self.Value.transform.position,
            Target.Value.transform.position,
            MoveSpeed.Value * Time.deltaTime);

            return Status.Success;
        }
        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

