using UnityEngine;

public class Change_Page : MonoBehaviour
{
    private GameObject _leftArrow;
    private GameObject _rightArrow;
    private Page_Number _pageNumber;
    private int _numberOfPages;
    private int _currentPage = 0;

    void Start()
    {
        _rightArrow = transform.GetChild(transform.childCount - 1).gameObject;
        _leftArrow = transform.GetChild(transform.childCount - 2).gameObject;
        _pageNumber = transform.GetChild(transform.childCount - 3).GetChild(0).GetComponent<Page_Number>();

        _numberOfPages = _pageNumber.GetNumberPages();

        _leftArrow.SetActive(false);
        if( _numberOfPages == 1 )
        {
            _rightArrow.SetActive(false);
        }
        else
        {
            _rightArrow.SetActive(true);
        }
    }

    public void NextPage()
    {
        if( _currentPage < _numberOfPages - 1 )
        {
            if(_currentPage == 0)
            {
                _leftArrow.SetActive(true);
            }

            _currentPage++;
            _pageNumber.SetPage(_currentPage);

            if(_currentPage ==  _numberOfPages - 1 )
            {
                _rightArrow.SetActive(false);
            }
        }
        else
        {
            _rightArrow.SetActive(false);
        }
    }

    public void PreviousPage()
    {
        if( _currentPage > 0)
        {
            if(_currentPage == _numberOfPages - 1)
            {
                _rightArrow.SetActive(true);
            }

            _currentPage--;
            _pageNumber.SetPage(_currentPage);

            if( _currentPage == 0 )
            {
                _leftArrow.SetActive(false);
            }
        }
    }
}
