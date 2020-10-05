using UnityEngine;

internal class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] public Color highlightColor;
    [SerializeField] public Color defaultColor;


    public void OnDeselect(Transform selection)
    {
        var _selectionSpriteRenderer = selection.GetComponent<SpriteRenderer>();
        if (_selectionSpriteRenderer != null)
        {
            _selectionSpriteRenderer.color = this.defaultColor;
        }

    }

    public void OnSelect(Transform selection)
    {
        var _selectionSpriteRenderer = selection.GetComponent<SpriteRenderer>();
        if (_selectionSpriteRenderer != null)
        {
            _selectionSpriteRenderer.color = this.highlightColor;
        }

    }
}
