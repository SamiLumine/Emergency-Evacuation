using UnityEngine;
using UnityEngine.UI;

public class AutoSize_Content_ScrollView : MonoBehaviour
{
    [SerializeField]
    float _fixePadding = 5f;

    private bool _alreadyUpdated = false;

    public void InitContentSize()
    {
        if (!_alreadyUpdated)
        {

            Debug.Log("Size scroll view " + transform.parent.parent.parent.name);
            float newHeight = 0f;

            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform childRect = transform.GetChild(i).GetComponent<RectTransform>();

                //childRect.ForceUpdateRectTransforms();

                newHeight += childRect.rect.height;
                Debug.Log("child " + childRect.name + " y: " + childRect.rect.y + ", height: " + childRect.rect.height);

                if (childRect.GetComponent<ContentSizeFitter>() != null)
                {
                    Debug.Log("content size filter started: " + childRect.GetComponent<ContentSizeFitter>().didStart);
                }

                if (i < transform.childCount - 2)
                {
                    newHeight += _fixePadding;
                }
            }

            RectTransform rect = GetComponent<RectTransform>();
            //newHeight -= rect.anchoredPosition.y / 2f;

            Debug.Log("New Height: " + newHeight + ", old height: " + rect.sizeDelta.y);
            if (newHeight > rect.sizeDelta.y)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);

                ScrollRect scrollRect = transform.parent.parent.GetComponent<ScrollRect>();
                if (scrollRect != null)
                {
                    Debug.Log("old Scrollbar value: " + scrollRect.verticalScrollbar.value);
                    //scrollRect.verticalScrollbar.value = 0.5f;
                    Debug.Log("new Scrollbar value: " + scrollRect.verticalScrollbar.value);
                }
            }
            _alreadyUpdated = true;
        }
    }
}
