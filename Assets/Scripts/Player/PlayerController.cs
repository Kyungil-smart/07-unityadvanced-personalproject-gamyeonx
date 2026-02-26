using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float _moveSpeed;
    [SerializeField] public float _jumpHeight;

    [SerializeField] Animator _animator;

    private Rigidbody2D _rigidbody; 
    private Vector2 _moveInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;

    private bool _runInput = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _runAction = InputSystem.actions["Run"];
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMove;
        _moveAction.canceled += MoveCancel;
        //_jumpAction.started += OnJump;
        //_jumpAction.canceled += JumpCancel;
        _runAction.started += OnRun;
        _runAction.canceled += RunCancel;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInput.x * _moveSpeed, _rigidbody.linearVelocity.y);
    }

    private void Update()
    {

        if (_moveInput.x != 0)
        {
            if (_moveInput.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (_moveInput.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        _animator.SetFloat("MoveSpeed", Mathf.Abs(_moveInput.x));
        _animator.SetBool("IsRun", _runInput);
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= MoveCancel;
        //_jumpAction.started -= OnJump;
        //_jumpAction.canceled -= JumpCancel;
        _runAction.started -= OnRun;
        _runAction.canceled -= RunCancel;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    public void MoveCancel(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {

    }

    public void JumpCancel(InputAction.CallbackContext ctx)
    {

    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            _runInput = true;
            _moveSpeed += 2;
        }    
    }

    public void RunCancel(InputAction.CallbackContext ctx)
    {
        if(ctx.canceled)
        {
            _runInput = false;
            _moveSpeed -= 2;
        }
    }
}
