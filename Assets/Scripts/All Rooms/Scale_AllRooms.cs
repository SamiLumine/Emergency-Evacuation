using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.U2D;

public class Scale_AllRooms : MonoBehaviour
{
    [Tooltip("Reference to the Floor_Plane object used to detect the current AR floor")]
    [SerializeField]
    private Floor_Plane _floorPlane;

    [Tooltip("Reference to the Player_Room script used to set the player's current room")]
    [SerializeField]
    private Player_Room _playerRoom;

    private List<GameObject> _allRoomsFloor = new List<GameObject>();
    private Dictionary<GameObject, Vector3> _allRoomsFloorDefaultCoordinate = new Dictionary<GameObject, Vector3>();

    private bool _currentFloorFound = false;
    private GameObject _currentARFloor = null;
    private GameObject _currentAllRoomFloor = null;

    void Start()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Transform child = transform.GetChild(0).GetChild(i);

            if (child.CompareTag("Start"))
            {
                for (int j = 0; j < child.childCount; j++)
                {
                    Transform grandChild = child.GetChild(j);

                    if (grandChild.CompareTag("Floor"))
                    {
                        _allRoomsFloor.Add(grandChild.gameObject);
                        _allRoomsFloorDefaultCoordinate.Add(grandChild.gameObject, grandChild.transform.position);
                    }
                }
            }
        }
    }

    void Update()
    {
        if(!_currentFloorFound)
        {
            _currentARFloor = _floorPlane.GetFloorPlane();
            if (_currentARFloor != null)
            {
                _currentFloorFound = true;
                SearchMatchingFloor();
                ApplyRoomPlayer();
            }
        }
    }

    private void SearchMatchingFloor()
    {
        List<float> floatList = new List<float>();

        foreach (var plane in _allRoomsFloor)
        {
            Mesh meshAllRoomFloor = plane.GetComponent<MeshFilter>().sharedMesh;
            Mesh meshARFloor = _currentARFloor.GetComponent<MeshFilter>().sharedMesh;
            if (meshAllRoomFloor != null && meshARFloor != null)
            {
                float cumulDistance = 0;

                for (int i = 0; i < meshARFloor.vertexCount; i++)
                {
                    float min = float.MaxValue;

                    for (int j = 0; j < meshAllRoomFloor.vertexCount; j++)
                    {
                        float distance = Vector3.Distance(meshARFloor.vertices[i], meshAllRoomFloor.vertices[j]);

                        if(distance <  min)
                        {
                            min = distance;
                        }
                    }

                    cumulDistance += min;
                }

                floatList.Add(cumulDistance);
            }

        }

        float minVal = float.MaxValue;
        int pos = 0;

        for (int i = 0; i < floatList.Count; i++)
        {
            if (floatList[i] < minVal)
            {
                minVal = floatList[i];
                pos = i;
            }
        }

        _currentAllRoomFloor = _allRoomsFloor[pos];
        Debug.Log("Closest Floor is: " +  _currentAllRoomFloor + " with score of: " + floatList[pos]);
        MoveAllRoom();
    }

    public void MoveAllRoom()
    {
        transform.GetChild(0).transform.localPosition = -transform.GetChild(0).transform.InverseTransformPoint(_currentAllRoomFloor.transform.position);

        Debug.Log("Floor in AllRoom: " + transform.InverseTransformPoint(_currentAllRoomFloor.transform.position));

        transform.parent = _currentARFloor.transform;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        Transform pos = _currentAllRoomFloor.transform;
        Vector3 angle = Vector3.zero;

        while (pos.parent != transform)
        {
            angle += pos.localEulerAngles;
            pos = pos.parent;
        }

        transform.localEulerAngles = -angle;
        transform.parent = null;
    }

    private void ApplyRoomPlayer()
    {
        Debug.Log("Before Set");
        var Room = _currentAllRoomFloor.GetComponent<Room_Starting_Room>().GetRoom();
        Debug.Log("Room: " +  Room);
        _playerRoom.SetCurrentRoom(Room, null, false);
        Debug.Log("After Set");
    }

    public void GetPlayerCoordinateInDefautlWorld()
    {
        if( _currentAllRoomFloor != null)
        {
            Vector3 floorCordinate = _allRoomsFloorDefaultCoordinate[_currentAllRoomFloor];
            Vector3 playerCordinate = _floorPlane.transform.parent.position;

            Vector3 coordinateInDefaultWorld = floorCordinate + _currentAllRoomFloor.transform.InverseTransformPoint(playerCordinate);
            Debug.Log("Position in world of player: (" + coordinateInDefaultWorld.x + ", " + coordinateInDefaultWorld.y + ", " + coordinateInDefaultWorld.z + ")");
        }
        else
        {
            Debug.Log("No Floor attribuated");
        }

    }
}
