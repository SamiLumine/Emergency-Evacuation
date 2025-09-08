using System.Collections.Generic;
using UnityEngine;

public class Graph_link : MonoBehaviour
{
    private static float _weightDistanceFactor = 1f;

    [Tooltip("If true, all link weights between doors will be set to 0. " +
           "This should be used for the Exit GameObject, where all exit doors are placed. " +
           "Otherwise, weights are calculated based on the distance between doors.")]
    [SerializeField]
    private bool _isWeightNull = false;

    private Dictionary<(Door, Door), float> _weight;

    void Start()
    {
        Door[] doors = GetComponent<Room>().GetDoors();
        int size = doors.Length;

        _weight = new Dictionary<(Door, Door), float>();

        for (int i= 0; i < size; i++)
        {
            for(int j = 0;  j < i; j++)
            {
                if (!_isWeightNull)
                {
                    _weight.Add((doors[i], doors[j]), _weightDistanceFactor * Vector3.Distance(doors[i].transform.position, doors[j].transform.position));
                    _weight.Add((doors[j], doors[i]), _weightDistanceFactor * Vector3.Distance(doors[i].transform.position, doors[j].transform.position));
                }
                else
                {
                    Debug.Log("Creating link between: (" + doors[i] + ", " + doors[j] + ")");
                    _weight.Add((doors[i], doors[j]), 0);
                    _weight.Add((doors[j], doors[i]), 0);
                }
            }
        }
        
    }

    public float GetLinkWeight(Door door1, Door door2)
    {
        return _weight[(door1, door2)];
    }
}
