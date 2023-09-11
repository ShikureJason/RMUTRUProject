using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "GameManager/Input Reader")]
public class InputReaderSO : ScriptableObject, InputManager.IOnGameplayActions, InputManager.IOnDialogueActions, InputManager.IOnMenuActions
{
    public UnityAction<Vector2> MoveEvent;
    public UnityAction JumpEvent;
    public UnityAction<bool> SprintEvent;
    public UnityAction<Vector2> RotateCamEvent;
    public UnityAction InteractEvent;
    public UnityAction<bool> ControlCamEvent;
    public UnityAction EscapeGameEvent;

    public UnityAction NextDialogueEvent;

    public UnityAction EscapeMenuEvent;

    private InputManager _inputManager;

    private void OnEnable()
    {
        _inputManager = new InputManager();
        _inputManager.OnGameplay.SetCallbacks(this);
        _inputManager.OnDialogue.SetCallbacks(this);
        _inputManager.OnMenu.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
			SprintEvent.Invoke(true);
		if (context.phase == InputActionPhase.Canceled)
			SprintEvent.Invoke(false);
    }

    public void OnRotateCam(InputAction.CallbackContext context)
    {
        RotateCamEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            InteractEvent.Invoke();
    }

    public void OnEscapeGame(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            EscapeGameEvent.Invoke();
    }

    public void OnControlCam(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ControlCamEvent.Invoke(true);
        if (context.phase == InputActionPhase.Canceled)
            ControlCamEvent.Invoke(false);
    }

    public void OnNextDialogue(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            NextDialogueEvent.Invoke();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            EscapeMenuEvent.Invoke();
    }

    public void EnableGameplay()
    {
        _inputManager.OnGameplay.Enable();
    }

    public void EnableMenu()
    {
        _inputManager.OnGameplay.Disable();
        _inputManager.OnMenu.Enable();
    }
    
    public void DisableMenu()
    {
        _inputManager.OnMenu.Disable();
        _inputManager.OnGameplay.Enable();
    }

    public void DisableAllInput()
    {
        _inputManager.OnGameplay.Disable();
        _inputManager.OnDialogue.Disable();
    }

    public void EnableDialogueInput()
    {
        _inputManager.OnGameplay.Disable();
        _inputManager.OnDialogue.Enable();
    }

    public void DisableDialogueInput()
    {
        _inputManager.OnDialogue.Disable();
        _inputManager.OnGameplay.Enable();
    }

}
