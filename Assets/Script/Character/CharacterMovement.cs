using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader = default;
    [Header("Setting")]
    [SerializeField] private float _normalSpeed = default;
    [SerializeField] private float _sprintSpeed = default;
    [SerializeField] private float _jumpForce = default;

    private Vector2 diraction;
    private Vector3 jumpVector;
    private CharacterController controller;
    private Transform cameraTransform;
    private float gravity = -9.18f;
    private bool isSprint = false;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += Move;
        _inputReader.JumpEvent += Jump;
        _inputReader.SprintEvent += Sprint;

    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= Move;
        _inputReader.JumpEvent -= Jump;
        _inputReader.SprintEvent -= Sprint;
    }

    private void Update()
    {
        // Rotate the movement input based on the camera's rotation
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = cameraRight.y = 0f;
        Vector3 adjustedMovement = cameraRight.normalized * diraction.x + cameraForward.normalized * diraction.y;
        float speed = isSprint ? _sprintSpeed : _normalSpeed;
        adjustedMovement.y += gravity * Time.deltaTime;
        if (adjustedMovement.x != 0 && adjustedMovement.z != 0)
        {
            //transform.forward = adjustedMovement;
            Quaternion targetRotation = Quaternion.LookRotation(adjustedMovement);
            transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        }
        if (!controller.isGrounded)
        {
            jumpVector.y += gravity * Time.deltaTime;
        }

        controller.Move((adjustedMovement * speed + jumpVector) * Time.deltaTime);
    }

    private void Move(Vector2 diraction)
    {
        this.diraction = diraction;
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            jumpVector = Vector3.up * _jumpForce;
        }           
           
    }

    private void Sprint(bool isSprint)
    {
        this.isSprint = isSprint;
    }

    public Transform GetTransform => transform;
}
