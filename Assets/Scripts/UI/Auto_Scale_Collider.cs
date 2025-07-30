using UnityEngine;

public class Auto_Scale_Collider : MonoBehaviour
{

    void Start()
    {
        Canvas.ForceUpdateCanvases();


        BoxCollider [] colliders = GetComponentsInChildren<BoxCollider>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 size = new Vector3(rectTransform.rect.size.x, rectTransform.rect.size.y, 2);
        foreach (BoxCollider collider in colliders)
        {
            collider.size = size;
        }
    }


}
