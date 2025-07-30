using UnityEngine;

public class Follow_player : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Player_height _player_Height;

    void Update()
    {

        if( _camera != null && (_camera.transform.position.x != transform.position.x || _camera.transform.position.z != transform.position.z))
        {
            var newPos = _camera.transform.position;
            newPos.y = _camera.transform.position.y - _player_Height.GetPlayerHeight();

            transform.position = newPos;
        }
    }
}
