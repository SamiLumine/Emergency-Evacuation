using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Number : MonoBehaviour
{

    [SerializeField]
    private float _spacingBetweenDots = 7f;

    private int _numberOfPages = 1;
    // tuple: (Page so show, Dot unfilled, Dot filled)
    private List<(GameObject, AutoSize_Content_ScrollView, RawImage, RawImage)> _allDots = new List<(GameObject, AutoSize_Content_ScrollView, RawImage, RawImage)>();
    private (GameObject, AutoSize_Content_ScrollView, RawImage, RawImage) _currentPage = (null, null, null, null);
    private int _currentPageIndex = 0;

    private bool _initiated = false;

    void Start()
    {
        if(!_initiated)
        {
            _numberOfPages = transform.parent.parent.childCount - 3;

            GameObject dot = transform.GetChild(0).gameObject;

            if (_numberOfPages == 1)
            {
                Destroy(dot);
            }
            else
            {
                _currentPage = (transform.parent.parent.GetChild(0).gameObject, transform.parent.parent.GetChild(0).GetComponentInChildren<AutoSize_Content_ScrollView>(true), 
                                dot.transform.GetChild(0).GetComponent<RawImage>(), dot.transform.GetChild(1).GetComponent<RawImage>());
                _currentPage.Item1.SetActive(true);
                _allDots.Add(_currentPage);

                RectTransform dotTransform = dot.GetComponent<RectTransform>();
                dotTransform.ForceUpdateRectTransforms();
                float dotWidth = dotTransform.rect.width + _spacingBetweenDots;
                float xPos = dotTransform.localPosition.x + dotWidth;

                for (int i = 1; i < _numberOfPages; i++)
                {
                    (GameObject, AutoSize_Content_ScrollView, RawImage, RawImage) lineElem;
                    lineElem.Item1 = transform.parent.parent.GetChild(i).gameObject;
                    lineElem.Item1.SetActive(false);
                    lineElem.Item2 = lineElem.Item1.GetComponentInChildren<AutoSize_Content_ScrollView>();

                    GameObject newDot = Instantiate(dot, dot.transform.parent);
                    lineElem.Item3 = newDot.transform.GetChild(0).GetComponent<RawImage>();
                    lineElem.Item3.enabled = true;
                    lineElem.Item4 = newDot.transform.GetChild(1).GetComponent<RawImage>();
                    lineElem.Item4.enabled = false;
                    _allDots.Add(lineElem);

                    RectTransform newDotTransform = newDot.GetComponent<RectTransform>();
                    Vector3 newPos = newDotTransform.localPosition;
                    newPos.x = xPos;
                    newDotTransform.localPosition = newPos;

                    xPos += dotWidth;
                }

                _currentPage.Item3.enabled = false;
                _currentPage.Item4.enabled = true;
            }

            _initiated = true;
        }
    }

    public int GetNumberPages()
    {
        return _numberOfPages;
    }

    public int GetCurrentPageIndex()
    {
        return _currentPageIndex;
    }

    public void SetPage(int page)
    {
        if (_numberOfPages > 1 && page >= 0 && page < _numberOfPages)
        {
            _currentPage.Item4.enabled = false;
            _currentPage.Item3.enabled = true;
            _currentPage.Item1.SetActive(false);

            _currentPageIndex = page;
            _currentPage = _allDots[page];

            _currentPage.Item4.enabled = true;
            _currentPage.Item3.enabled = false;
            _currentPage.Item1.SetActive(true);
            _currentPage.Item2.InitContentSize();
        }
    }

    public bool hasBeenInitiated()
    {
        return _initiated;
    }
}