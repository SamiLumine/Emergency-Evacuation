using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Player_evacuation : MonoBehaviour
{
    [SerializeField]
    private Player_Room _playerRoom;

    private Door _exitDoor;
    private Door _nextDoor;

    private List<Door> _doorPathEvacuation = null;
    private int _indexInList;

    private bool _activated = true;

    private void Start()
    {
        _exitDoor = GetComponentInChildren<Door>();

        End_Program.GetStopEvent().AddListener(StopEvacuation);
    }

    public Door GetNextDoor()
    {
        return _nextDoor;
    }

    private bool CheckIfDoorInDoors(Door door, Door[] doors)
    {
        int i;

        for(i = 0; i < doors.Length && door != doors[i]; ++i){}

        return i != doors.Length && door == doors[i];
    }

    private void Dijkstra()
    {
        SortedList<float, Door> distanceFromExit = new SortedList<float, Door>(new DuplicateKeyComparer<float>());
        Dictionary<Door, Door> doorPredecessor = new Dictionary<Door, Door>();
        foreach (Door door in transform.parent.GetComponentsInChildren<Door>(true))
        {
            if (door != _exitDoor)
            {
                distanceFromExit.Add(float.MaxValue, door);
            }
            doorPredecessor.Add(door, null);
        }

        Door[] playerRoomDoors = _playerRoom.GetCurrentPlayerRoom().GetDoors();
        Door currentNode = _exitDoor;
        float currentNodeWeight = 0f;
        int nbIté = 0;


        while (!CheckIfDoorInDoors(currentNode, playerRoomDoors))
        {
            if(currentNode.GetRoom1() != null)
            {
                Door[] allNodes = currentNode.GetRoom1().GetDoors();

                foreach (Door door in allNodes)
                {
                    if (distanceFromExit.ContainsValue(door))
                    {
                        float newDistance = currentNodeWeight + currentNode.GetRoom1().GetGraphLink().GetLinkWeight(currentNode, door);

                        Debug.Log("new distance calculated between " + currentNode.gameObject.name + " and " + door.gameObject.name + "is " + newDistance);
                        if (newDistance < distanceFromExit.ElementAt(distanceFromExit.IndexOfValue(door)).Key)
                        {
                            Debug.Log("Distnace updated from " + distanceFromExit.ElementAt(distanceFromExit.IndexOfValue(door)).Key + " to " + newDistance);
                            distanceFromExit.RemoveAt(distanceFromExit.IndexOfValue(door));
                            distanceFromExit.Add(newDistance, door);
                            doorPredecessor[door] = currentNode;
                        }
                    }
                }
            }

            if (currentNode.GetRoom2() != null)
            {
                Door[] allNodes = currentNode.GetRoom2().GetDoors();

                foreach (Door door in allNodes)
                {
                    if (distanceFromExit.ContainsValue(door))
                    {
                        float newDistance = currentNodeWeight + currentNode.GetRoom2().GetGraphLink().GetLinkWeight(currentNode, door);

                        Debug.Log("new distance calculated between " + currentNode.gameObject.name + " and " + door.gameObject.name + "is " + newDistance);
                        if (newDistance < distanceFromExit.ElementAt(distanceFromExit.IndexOfValue(door)).Key)
                        {
                            Debug.Log("Distnace updated from " + distanceFromExit.ElementAt(distanceFromExit.IndexOfValue(door)).Key + " to " + newDistance);
                            distanceFromExit.RemoveAt(distanceFromExit.IndexOfValue(door));
                            distanceFromExit.Add(newDistance, door);
                            doorPredecessor[door] = currentNode;
                        }
                    }
                }
            }

            /*Debug.Log("Itération: " + nbIté);
            foreach(KeyValuePair<float,Door> keyValuePair in distanceFromExit)
            {
                Debug.Log("Distance: " + keyValuePair.Key + ", Door: " + keyValuePair.Value.gameObject.name);
            }*/

            currentNode = distanceFromExit.ElementAt(0).Value;
            currentNodeWeight = distanceFromExit.ElementAt(0).Key;
            distanceFromExit.RemoveAt(0);
            nbIté++;
        }



        _doorPathEvacuation = new List<Door>();
        _indexInList = 0;
        _nextDoor = currentNode;
        Debug.Log("Path to exit:");
        int i = 0;
        while(currentNode != _exitDoor)
        {
            _doorPathEvacuation.Add(currentNode);
            Debug.Log("Door " + i + ": " + currentNode.gameObject.name);
            currentNode = doorPredecessor[currentNode];
            i++;
        }
    }

    public void UpdateNextDoor()
    {
        if (_activated)
        {

            Room currentRoom = _playerRoom.GetCurrentPlayerRoom();
            Door[] currentDoors = currentRoom.GetDoors();

            Debug.Log("Updating with room: " + currentRoom.gameObject.name);
            if (_doorPathEvacuation != null && _indexInList < _doorPathEvacuation.Count - 1 && CheckIfDoorInDoors(_doorPathEvacuation[_indexInList + 1], currentDoors))
            {
                Debug.Log("Moving forward");
                _nextDoor.GetDoorIndication().StopIndicatingDoor();
                _nextDoor = _doorPathEvacuation[_indexInList + 1];
                _indexInList++;
                _nextDoor.GetObjectTarget().StartFollowing();
                _nextDoor.GetDoorIndication().StartIndicatingDoor();

            }
            else if (_doorPathEvacuation != null && _indexInList > 0 && CheckIfDoorInDoors(_doorPathEvacuation[_indexInList - 1], currentDoors))
            {
                Debug.Log("Backtrack detected");
                _nextDoor.GetDoorIndication().StopIndicatingDoor();
                _nextDoor = _doorPathEvacuation[_indexInList - 1];
                _indexInList--;
                _nextDoor.GetObjectTarget().StartFollowing();
                _nextDoor.GetDoorIndication().StartIndicatingDoor();

            }
            else if (_doorPathEvacuation == null || !CheckIfDoorInDoors(_nextDoor, currentDoors))
            {
                if (_doorPathEvacuation != null)
                {
                    _nextDoor.GetDoorIndication().StopIndicatingDoor();
                }

                Debug.Log("Launch Dijkstra");
                Dijkstra();
                _nextDoor.GetObjectTarget().StartFollowing();
                _nextDoor.GetDoorIndication().StartIndicatingDoor();

            }

        }
    }

    public void StopEvacuation()
    {
        _activated = false;
    }
}

public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : IComparable
{
    #region IComparer<TKey> Members

    public int Compare(TKey x, TKey y)
    {
        int result = x.CompareTo(y);

        if (result == 0)
            return 1;   // Handle equality as beeing greater
        else
            return result;
    }

    #endregion
}
