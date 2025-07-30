using System.Collections.Generic;
using UnityEngine;

public class Object_Follow : MonoBehaviour
{
    private Transform _currentFollow = null;
    private bool _isVisible = false;
    private List<MeshRenderer> _compassVisual = new List<MeshRenderer>();

    private void Start()
    {
        foreach (MeshRenderer renderer in transform.parent.GetComponentsInChildren<MeshRenderer>(true))
        {
            _compassVisual.Add(renderer);
            renderer.enabled = false;
        }
    }

    void Update()
    {
        if (_currentFollow != null)
        {
            if(!_isVisible)
            {
                foreach(MeshRenderer renderer in  _compassVisual)
                {
                    renderer.enabled = true;
                }

                _isVisible = true;
            }

            transform.LookAt(_currentFollow.position);

            var rotation = transform.eulerAngles;
            rotation.x = 0;
            rotation.y += 180;
            rotation.z = 0;

            transform.eulerAngles = rotation;
        }
        else if (_isVisible)
        {
            foreach (MeshRenderer renderer in _compassVisual)
            {
                renderer.enabled = false;
            }
            _isVisible = false;
        }
    }

    public void ChangeObjectFollow(Transform obj)
    {
        _currentFollow = obj;
    }
}
