using UnityEngine;

public class Fix_Position_to_Camera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    [Range(0f, 1f)]
    private float _distance = 0.5f;

    private Transform _rotateObject; 

    private bool _tracking = false;

    private Vector3 _position;
    private Vector3 _positionRotation;
    private Quaternion _rotation;

    private void Start()
    {
        _rotateObject = transform.GetChild(0);
        Check_Application_Started.GetEvent().AddListener(StartTracking);
    }

    void Update()
    {
        if (Debuging.IsDebuging() || _tracking)
        {
            TrackingCamera();
        }
    }

    private void TrackingCamera()
    {
        transform.position = _position + _camera.transform.position;

        _rotateObject.rotation = _rotation;
        _rotateObject.position = _positionRotation + _camera.transform.position;
    }

    public void StartTracking()
    {
        GameObject obj = new GameObject("Get Position");
        obj.transform.parent = _camera.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.up = Vector3.up;

        _position = obj.transform.TransformPoint(0, 0, _distance);
        _position -= _camera.transform.position;
        _positionRotation = _camera.transform.forward * _distance;
        _rotation = Quaternion.LookRotation(_positionRotation);
        
        _tracking = true;
        Destroy(obj);
    }

    public void StopTracking()
    {
        _tracking = false;
    }
}
