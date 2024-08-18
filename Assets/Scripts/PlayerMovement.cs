using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float WalkSpeed = 5f;
    public float SprintSpeed = 10f;

    public float GravityScaleMultiplier = 1f;
    public float GravityScale = -9.81f;

    public float GroundDrag;
    public float JumpForce;
    public float AirMultiplier;

    public float TurnSmoothTime = 0.1f;

    [Header("Ground Check")]
    public LayerMask whatIsGround;

    [Header("Key Binds")]
    readonly KeyCode sprint = KeyCode.LeftShift;
    readonly KeyCode jump = KeyCode.Space;

    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform cam;

    private Vector3 _playerVelocity;
    private Vector3 _direction;
    private Vector3 _moveDir;
    private float _YAxisVel;
    public bool _isGrounded;
    private float _currentSpeed;
    private float _turnSmoothVelocity;

    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        OnInput();
        ApplyRotation();
        ApplyMovement();
        GroundCheck();
    }
    private void OnInput()
    {
        //set current speed
        if (Input.GetKey(sprint))
            _currentSpeed = SprintSpeed;
        else
            _currentSpeed = WalkSpeed;

        //jump
        if (Input.GetKeyDown(jump) && _isGrounded)
            Jump();
    }
    private void ApplyMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _direction = new Vector3(horizontal, 0, vertical).normalized;

        //on ground
        if (_isGrounded)
            _playerVelocity = _moveDir * _currentSpeed;
        //in air
        else
            _playerVelocity = _moveDir * _currentSpeed * AirMultiplier;

        //apply gravity
        ApplyGravity();

        if (_direction.magnitude != 0f)
            _characterController.Move(_playerVelocity * Time.deltaTime);

    }
    private void ApplyRotation()
    {
        if (_direction.sqrMagnitude == 0) return;

        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, TurnSmoothTime);
        _moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, _characterController.height / 2 + 0.1f, whatIsGround))
            _isGrounded = true;
        else
            _isGrounded = false;
    }
    private void ApplyGravity()
    {
        if (_isGrounded && _YAxisVel < 0.0f)
            _YAxisVel = -1.0f;
        else
            _YAxisVel += GravityScaleMultiplier * GravityScale * Time.deltaTime;

        _playerVelocity.y = _YAxisVel;
    }
    private void Jump()
    {
        _playerVelocity.y += JumpForce;
    }
}
