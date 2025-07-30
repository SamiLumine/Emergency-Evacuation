using UnityEngine;

public class Object_Target : MonoBehaviour
{
    [SerializeField]
    private bool _following;

    [SerializeField]
    private Object_Follow _objectFollow;

    void Start()
    {
        if(_following)
        {
            _objectFollow.ChangeObjectFollow(transform);
        }

        End_Program.GetStopEvent().AddListener(RemoveFollowing);
    }

    public void StartFollowing()
    {
        _objectFollow.ChangeObjectFollow(transform);
        _following = true;
    }

    public void StopFollowing()
    {
        _following = false;
    }

    public bool isFollowing()
    {
        return _following;
    }

    private void RemoveFollowing()
    {
        if( _following )
        {
            _following = false;
            _objectFollow.ChangeObjectFollow(null);
        }
    }
}
