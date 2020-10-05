using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string hoverableTag = "Hoverable";
    //[SerializeField ] private Color highlightColor;
    //[SerializeField ] private Color defaultColor;

    private ISelectionResponse _selectionResponse;
    private IHoverResponse _hoverResponse;

    private Transform _selection;
    private Transform _hovered;

    private void Awake()
    {
        Input.simulateMouseWithTouches = false;

        _selectionResponse = GetComponent<ISelectionResponse>();
        _hoverResponse = GetComponent<IHoverResponse>();
    }

    public void Update()
    {
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }

        if (_hovered != null)
        {
            _hoverResponse.OnExitHover(_hovered);
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        _selection = null;
        _hovered = null;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            } 
            else if (selection.CompareTag(hoverableTag))
            {
                _hovered = selection;

            }


        }

        if (_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
            
        }

        if (_hovered != null)
        {
            _hoverResponse.OnHover(_hovered);
        }


    }

}
