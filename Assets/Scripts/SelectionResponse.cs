using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] public Color highlightColor;
    [SerializeField] public Color defaultColor;
    [SerializeField] public Animator animator;
    [SerializeField] public int activeSceneIndex;
    [SerializeField] public string descriptorTag;

    private GameObject[] productDescriptors;

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

        bool touchReleased = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

        if (Input.GetMouseButtonUp(0) | touchReleased) 
        {

            StartCoroutine(SwithMaterialRoutine(selection));

        }

    }

    IEnumerator SwithMaterialRoutine(Transform selection)
    {
        animator.SetBool("FadeOut", true);
        animator.SetBool("FadeIn", false);

        yield return new WaitForSeconds(.6f);

        SwitchMaterial switchMaterial = selection.GetComponent<SwitchMaterial>();
        if (switchMaterial != null)
        {
            switchMaterial.SwithEnvironmentMaterial();
        }
        
        ActiveSceneDeterminer activeSceneDeterminer = selection.GetComponent<ActiveSceneDeterminer>();
        if (activeSceneDeterminer != null)
        {
            activeSceneIndex = activeSceneDeterminer.ActiveSceneIndex;
        }

        SceneController sceneController = GetComponent<SceneController>();
        if (sceneController != null)
        {
            sceneController.ChangeActiveScene(activeSceneIndex);
        }

        productDescriptors = GameObject.FindGameObjectsWithTag(descriptorTag);

        foreach (GameObject pd in productDescriptors)
        {
            Debug.Log("found something?");
            AnimationController animController = pd.GetComponent<AnimationController>();

            if (animController != null && !animController.IsHiding())
            {
                pd.SetActive(false);
                animController.HidePopOut();
            }

        }

        yield return new WaitForSeconds(.6f);

        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", true);
    }

}
