using System;
using UnityEngine;

public class Player_height : MonoBehaviour
{
    private float _playerHeight;
    private Camera _playerCamera;

    private void Start()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _playerHeight = _playerCamera.transform.position.y;
    }

    public float GetPlayerHeight()
    {
        return _playerHeight;
    }

    public void UpdatePlayerHeight(GameObject currentARFloor)
    {
        _playerHeight = Math.Abs(currentARFloor.transform.position.y - _playerCamera.transform.position.y);
    }
}
