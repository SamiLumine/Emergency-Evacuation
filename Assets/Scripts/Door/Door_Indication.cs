using UnityEngine;

public class Door_Indication : MonoBehaviour
{
    [SerializeField]
    [Range(0.001f, 2f)]
    private float _animationSpeed;

    private bool _isAnimationPlaying = false;
    private Animator _animator;
    private MeshRenderer _cylenderRenderer;
    private MeshRenderer _coneRenderer;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetFloat("Animation Speed", _animationSpeed);
        _coneRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        _coneRenderer.enabled = false;
        _cylenderRenderer = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        _cylenderRenderer.enabled = false;

        End_Program.GetStopEvent().AddListener(StopIndicator);
    }

    public void StartIndicatingDoor()
    {
        if(!_isAnimationPlaying)
        {
            _isAnimationPlaying = true;
            _coneRenderer.enabled = true;
            _cylenderRenderer.enabled = true;
            _animator.Play("Path Indicator");
        }
    }

    public void StopIndicatingDoor()
    {
        if (_isAnimationPlaying)
        {
            _isAnimationPlaying = false;
            _coneRenderer.enabled = false;
            _cylenderRenderer.enabled = false;
            _animator.Play("Idle");
        }
    }

    private void StopIndicator()
    {
        if (_isAnimationPlaying)
        {
            StopIndicatingDoor();
        }
    }
}
