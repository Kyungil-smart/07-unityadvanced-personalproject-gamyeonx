using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.Rendering;
using Unity.AppUI.Core;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Self] [Target] [DetectRange] [MoveSpeed] [PatrolDistance]", category: "Action", id: "113b7e9ba81ffbec63deaf180a275758")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> DetectRange;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    [SerializeReference] public BlackboardVariable<float> PatrolDistance;

    private Vector2 _startPos;
    private bool _initialized = false;
    private int _dir = 1;

    private float _stuckTimer = 0f;
    private Vector2 _lastCheckedPos;
    private const float StuckCheckInterval = 0.5f;
    private const float StuckThreshold = 0.05f;

    protected override Status OnStart()
    {
        if (!_initialized)
        {
            _startPos = Self.Value.transform.position;
            _lastCheckedPos = _startPos;
            _stuckTimer = 0f;
            _initialized = true;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector2.Distance(Self.Value.transform.position, Target.Value.transform.position);

        if (distance > DetectRange.Value)
        {
        Vector2 pos = Self.Value.transform.position;
        float left = _startPos.x - PatrolDistance.Value;
        float right = _startPos.x + PatrolDistance.Value;

            if (pos.x >= right) _dir = -1;
            if (pos.x <= left) _dir = 1;

            _stuckTimer += Time.deltaTime;
            if (_stuckTimer >= StuckCheckInterval)
            {
                if (Mathf.Abs(pos.x - _lastCheckedPos.x) < StuckThreshold)
                    _dir = -_dir;

                _lastCheckedPos = pos;
                _stuckTimer = 0f;
            }

            if (_dir > 0)
                Self.Value.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                Self.Value.transform.rotation = Quaternion.Euler(0, 180, 0);

            pos.x += _dir * MoveSpeed.Value * Time.deltaTime;
            Self.Value.transform.position = pos;

            return Status.Success;
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

