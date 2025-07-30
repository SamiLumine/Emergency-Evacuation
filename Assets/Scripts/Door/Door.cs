using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _exit;

    [SerializeField]
    private Room _roomForward = null;

    [SerializeField]
    private Room _roomBackward = null;

    private Object_Target _objectTarget;
    private Door_Indication _doorIndication;

    private void Start()
    {
        _objectTarget = GetComponent<Object_Target>();
        _doorIndication = GetComponentInChildren<Door_Indication>();
    }

    public Object_Target GetObjectTarget()
    {
        return _objectTarget;
    }

    public Door_Indication GetDoorIndication()
    {
        return _doorIndication;
    }

    public Room GetRoom1()
    {
        return _roomForward;
    }

    public void SetRoomForward(Room room)
    {
        _roomForward = room;
    }

    public Room GetRoom2()
    {
        return _roomBackward;
    }

    public void SetRoomBackward(Room room)
    {
        _roomBackward = room;
    }

    public bool IsExit()
    {
        return _exit;
    }


    public void DoorCollision(Collision collision)
    {

        if (collision.transform.CompareTag("Player"))
        {


            Room newRoom = null;
            var playerRoom = collision.rigidbody.GetComponent<Player_Room>();
            Vector3 directionToTarget = transform.position - collision.transform.parent.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget) % 360f;
            bool nothingToDo = true;
            bool roomForward = false;

            if (angle < 90f && angle > -90f && !(playerRoom.GetLastDoorUsed() == this && playerRoom.GetLastDoorUsedForward() == true))
            {
                Debug.Log("Porte derrière moi, angle: " + angle);
                newRoom = _roomForward;
                roomForward = true;
                nothingToDo = false;
            }
            else if(!(playerRoom.GetLastDoorUsed() == this && playerRoom.GetLastDoorUsedForward() == false))
            {
                Debug.Log("Porte devant moi, angle: " + angle);
                newRoom = _roomBackward;
                nothingToDo = false;
            }

            if(newRoom == null && _exit)
            {
                Debug.Log("Launching end of program");
                End_Program.GetStopEvent().Invoke();
            }
            else if(!nothingToDo)
            {
                playerRoom.SetCurrentRoom(newRoom, this, roomForward);
            }
        }
    }
}
