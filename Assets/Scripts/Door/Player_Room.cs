using System.Collections.Generic;
using UnityEngine;

public class Player_Room : MonoBehaviour
{
    [Tooltip("Material applied to doors when they are in their normal state (not in current room)")]
    [SerializeField]
    private Material _normalMaterial;

    [Tooltip("Material applied to doors of the current room the player is in")]
    [SerializeField]
    private Material _inRoomMaterial;

    [Tooltip("Material applied to exit doors")]
    [SerializeField]
    private Material _exitMaterial;

    [Tooltip("Reference to the Player_evacuation script, used to update the next door to use")]
    [SerializeField]
    private Player_evacuation _playerEvacuation;

    private Room _currentRoom = null;

    private Door _lastDoor = null;
    private bool _lastDoorForward = false;
    private bool _currentlyChanging = false;

    public Room GetCurrentPlayerRoom()
    {
        return _currentRoom;
    }

    public void SetCurrentRoom(Room room, Door doorUsed, bool roomForward)
    {
        if (!_currentlyChanging)
        {
            _currentlyChanging = true;

            _lastDoor = doorUsed;
            _lastDoorForward = roomForward;

            if (_currentRoom != null)
            {
                changeRoomDoorsMaterial(_normalMaterial);
            }

            _currentRoom = room;
            _playerEvacuation.UpdateNextDoor();

            changeRoomDoorsMaterial(_inRoomMaterial);

            _currentlyChanging = false;
        }
    }

    private void changeRoomDoorsMaterial(Material material)
    {
        Debug.Log("Current Room: " + _currentRoom);
        foreach (Door door in _currentRoom.GetDoors())
        {
            List<Material> materials = new List<Material>();
            if (!door.IsExit())
            {
                materials.Add(material);
            }
            else
            {
                materials.Add(_exitMaterial);
            }

            var renderer = door.transform.GetChild(0).GetComponent<MeshRenderer>();
            Debug.Log("Door " + door.gameObject.name + " material: " + materials[0].name );
            renderer.SetMaterials(materials);

        }
    }

    public Door GetLastDoorUsed()
    {
        return _lastDoor;
    }

    public bool GetLastDoorUsedForward()
    {
        return _lastDoorForward;
    }
}
