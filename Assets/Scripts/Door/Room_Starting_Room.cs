using UnityEngine;

public class Room_Starting_Room : MonoBehaviour
{
    [SerializeField]
    Room _room;

    public Room GetRoom()
    {
        return _room;
    }
}
