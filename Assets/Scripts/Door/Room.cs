using UnityEngine;

public class Room : MonoBehaviour
{
    [Tooltip("Name of the room (for identification purposes)")]
    [SerializeField]
    private string _name;

    [Tooltip("Doors associated with this room (assign all Door components connected to this room)")]
    [SerializeField]
    private Door[] _doors;

    private Graph_link _graphLink;

    private void Start()
    {
        _graphLink = GetComponent<Graph_link>();
    }

    public Door[] GetDoors()
    {
        return _doors;
    }

    public string GetName()
    {
        return _name;
    }

    public Graph_link GetGraphLink()
    {
        return _graphLink;
    }
}
