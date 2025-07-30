using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private string _name;

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
