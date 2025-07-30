using UnityEngine;
using System.Collections.Generic;

public class Object_Visibility : MonoBehaviour
{
    private List<MeshRenderer> _allRenderers = new List<MeshRenderer>(); 

    private bool _visible = false;

    void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if(!renderer.gameObject.CompareTag("Not Hide"))
            {
                _allRenderers.Add(renderer);
            }
        }

        SetVisibility(false);

        if (!Debuging.IsDebuging())
        {
            _allRenderers = null;
        }
    }

    private void SetVisibility(bool isVisible)
    {

        foreach( MeshRenderer renderer in _allRenderers )
        {
            if( renderer != null )
            {
                renderer.enabled = isVisible;
            }
        }
    }

    public void ToggleVisibility()
    {
        _visible = !_visible;
        SetVisibility(_visible);
    }

}
