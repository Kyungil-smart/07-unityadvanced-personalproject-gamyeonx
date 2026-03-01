using UnityEngine;
using UnityEngine.InputSystem;

public class BowCtrl : MonoBehaviour
{
    [SerializeField] private GameObject _arrowObject;

    private InputAction _attckAction;

    private void Awake()
    {
        _attckAction = InputSystem.actions["Attack"];
    }
    private void OnEnable()
    {
        _attckAction.started += AttackArrow;
    }

    private void OnDisable()
    {
        _attckAction.started -= AttackArrow;
    }

    public void AttackArrow(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;
        Vector3 backOffset = -transform.right * 0.3f;
        Instantiate(_arrowObject, transform.position + backOffset, transform.rotation);
    }
}
