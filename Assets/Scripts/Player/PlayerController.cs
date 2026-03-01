using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float _moveSpeed = 3;
    [SerializeField] public float _jumpHeight = 7;

    [SerializeField] Animator _animator;

    [SerializeField] private GameObject pauseUI;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _pauseAction;

    private LayerMask _jumpCheckLayer;
    [SerializeField] private float _rayDistance = 0.1f;

    private bool _runInput = false;

    private bool isPaused = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _runAction = InputSystem.actions["Run"];
        _pauseAction = InputSystem.actions["Pause"];
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMove;
        _moveAction.canceled += MoveCancel;
        _jumpAction.started += OnJump;
        _jumpAction.canceled += JumpCancel;
        _runAction.started += OnRun;
        _runAction.canceled += RunCancel;
        _pauseAction.started += OnPause;
    }

    private void Start()
    {
        _jumpCheckLayer = LayerMask.GetMask("Ground", "Monster");
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInput.x * _moveSpeed, _rigidbody.linearVelocity.y);
        IsGrounded();
    }

    private void Update()
    {
        if (_moveInput.x != 0)
        {
            if (_moveInput.x > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (_moveInput.x < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        _animator.SetFloat("MoveSpeed", Mathf.Abs(_moveInput.x));
        _animator.SetBool("IsRun", _runInput);
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= MoveCancel;
        _jumpAction.started -= OnJump;
        _jumpAction.canceled -= JumpCancel;
        _runAction.started -= OnRun;
        _runAction.canceled -= RunCancel;
        _pauseAction.started -= OnPause;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    public void MoveCancel(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, new Vector2(0.8f, 0.1f), 0f, Vector2.down, _rayDistance, _jumpCheckLayer);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        {
            if (ctx.started && IsGrounded())
            {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpHeight);
                _animator.SetTrigger("IsJump");
            }
        }
    }

    public void JumpCancel(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled && _rigidbody.linearVelocity.y > 0)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.4f);
        }
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _runInput = true;
            _moveSpeed += 2;
        }
    }

    public void RunCancel(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            _runInput = false;
            _moveSpeed -= 2;
        }
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * _rayDistance, new Vector3(0.8f, 0.1f, 0f));
    }
}
