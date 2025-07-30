using UnityEngine;
using UnityEngine.Events;

public class Check_Application_Started : MonoBehaviour
{
    [SerializeField]
    Camera _camera;

    private static UnityEvent _startedEvent = new UnityEvent();

    private bool _started = false;
    private Vector3 _defaultPosition = Vector3.zero;

    public static UnityEvent GetEvent()
    {
        return _startedEvent;
    }

    private void Start()
    {
        _defaultPosition = _camera.transform.position;
    }

    private void Update()
    {
        if(!_started && _defaultPosition - _camera.transform.position != Vector3.zero)
        {
            _started = true;
            _startedEvent.Invoke();
        }
    }
}
