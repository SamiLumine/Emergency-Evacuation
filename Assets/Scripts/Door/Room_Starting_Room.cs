using UnityEngine;

public class Room_Starting_Room : MonoBehaviour
{
    [Tooltip("Assign here the Room corresponding to the prefab associated with this starting room.")]
    [SerializeField]
    Room _room;

    public Room GetRoom()
    {
        return _room;
    }
}
