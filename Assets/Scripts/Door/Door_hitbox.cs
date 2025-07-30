using UnityEngine;

public class Door_hitbox : MonoBehaviour
{
    private Door _door;


    void Start()
    {
        _door = GetComponentInParent<Door>();
    }

    private void OnCollisionExit(Collision collision)
    {
        _door.DoorCollision(collision);
    }
}
