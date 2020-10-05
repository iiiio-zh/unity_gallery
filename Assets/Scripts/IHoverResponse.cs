using UnityEngine;

internal interface IHoverResponse
{
    void OnHover(Transform selection);
    void OnExitHover(Transform selection);
}
