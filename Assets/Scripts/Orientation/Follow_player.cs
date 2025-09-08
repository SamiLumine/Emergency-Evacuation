using UnityEngine;

public class Follow_player : MonoBehaviour
{
    [Tooltip("Reference to the Camera that represents the player")]
    [SerializeField]
    private Camera _camera;

    [Tooltip("Reference to the Player_height component. Used to position the GameObject on which this Follow_player script is attached at the player's feet.")]
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
