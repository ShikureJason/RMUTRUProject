using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader = default;
    [SerializeField] private Camera _camera = default;
    [SerializeField] private CinemachineFreeLook _freeLookVCam = default;
    [SerializeField][Range(.1f, 5f)] private float _speedMultiplier = 1f;

    private bool _isRMBPressed = false;

    private void OnEnable()
    {
        _inputReader.ControlCamEvent += OnControlCamera;
        _inputReader.RotateCamEvent += OnCameraRotate;
    }
    private void OnDisable()
    {
        _inputReader.ControlCamEvent -= OnControlCamera;
        _inputReader.RotateCamEvent -= OnCameraRotate;
    }

    private void OnControlCamera(bool isControl)
    {
        _isRMBPressed = isControl;
        Cursor.visible = !isControl;
        if (isControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            _freeLookVCam.m_XAxis.m_InputAxisValue = 0;
            _freeLookVCam.m_YAxis.m_InputAxisValue = 0;
        }
    }

    private void OnCameraRotate(Vector2 cameraMovement)
    {
        if (!_isRMBPressed)
            return;
        _freeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * Time.deltaTime * _speedMultiplier;
        _freeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * Time.deltaTime * _speedMultiplier;
    }

}
