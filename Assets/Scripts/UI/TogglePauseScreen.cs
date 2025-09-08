using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TogglePauseScreen : MonoBehaviour
{
    private static bool _menuActivated = false;
    private static UnityEvent<bool> _pauseToggled = new UnityEvent<bool>();

    [Tooltip("Input Action used to toggle the pause screen on and off")]
    [SerializeField]
    private InputActionReference _pauseScreenAction;

    [Tooltip("Reference to the GameObject that represents the pause screen UI")]
    [SerializeField]
    private GameObject _pauseScreen;

    private Pause_Screen_Selection _pauseScreenSelection;
    private Fix_Position_to_Camera _fix_Position_To_Camera;

    public static bool PauseScreenState()
    {
        return _menuActivated;
    }

    public static UnityEvent<bool> GetPauseToggleEvent()
    {
        return _pauseToggled;
    }

    void Start()
    {
        _fix_Position_To_Camera = GetComponentInParent<Fix_Position_to_Camera>();
        _pauseScreenSelection = GetComponentInChildren<Pause_Screen_Selection>(includeInactive: true);

        _pauseScreenAction.action.performed += OnTogglePauseScreen;
    }

    public void OnTogglePauseScreen(InputAction.CallbackContext context)
    {
        PauseScreenToggle();
    }

    public void PauseScreenToggle()
    {

        _menuActivated = !_menuActivated;
        _pauseToggled.Invoke(_menuActivated);

        if (_menuActivated)
        {
            _fix_Position_To_Camera.StartTracking();
        }
        else
        {
            _fix_Position_To_Camera.StopTracking();
            _pauseScreenSelection.ResetPause();
        }

        _pauseScreen.SetActive(_menuActivated);
    }

    private void OnDestroy()
    {
        _pauseScreenAction.action.performed -= OnTogglePauseScreen;
    }
}
