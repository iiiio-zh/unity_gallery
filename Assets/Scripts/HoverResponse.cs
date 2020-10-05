using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverResponse : MonoBehaviour, IHoverResponse
{
    [SerializeField] public GameObject playerCamera;
    [SerializeField] public Color highlightColor;
    [SerializeField] public Color defaultColor;
    [SerializeField] public string descriptionTag;

    [SerializeField] public float focusDamping = 0.01f;
    [SerializeField] public float toleranceAngle = 10f;

    private float timeCount = 0.0f;

    private bool focusing = false;
    private Transform selectedToBeFocused;

    private GameObject popOutObject;
    private AnimationController popOutObjectAnimController;

    public void OnExitHover(Transform selection)
    {
        var _selectionSpriteRenderer = selection.GetComponent<SpriteRenderer>();
        if (_selectionSpriteRenderer != null)
        {
            _selectionSpriteRenderer.color = this.defaultColor;
        }

    }

    public void OnHover(Transform selection)
    {
        var _selectionSpriteRenderer = selection.GetComponent<SpriteRenderer>();
        if (_selectionSpriteRenderer != null)
        {
            _selectionSpriteRenderer.color = this.highlightColor;
        }

        bool touchReleased = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

        if (Input.GetMouseButtonUp(0) | touchReleased && selection.gameObject && selection.gameObject.activeSelf)
        {
            foreach (Transform child in selection)
            {
                AnimationController animController = child.gameObject.GetComponent<AnimationController>();
                if (child.CompareTag(descriptionTag) && animController != null && (!child.gameObject.activeSelf || animController.IsHiding()))
                {
                    focusing = true;
                    selectedToBeFocused = selection;

                    //child.gameObject.SetActive(true);
                    //animController.ShowPopOut();
                    popOutObject = child.gameObject;
                    popOutObjectAnimController = animController;

                }
                else if (child.CompareTag(descriptionTag) && animController != null && !animController.IsHiding())
                {
                    animController.HidePopOut();

                }

            }
            

        }

    }

    private void Update()
    {

        if (focusing && selectedToBeFocused != null)
        {

            FirstPersonCamera firstPersonCamera = playerCamera.GetComponent<FirstPersonCamera>();
            if (firstPersonCamera != null)
            {
                firstPersonCamera.DisableCameraMovement();
            }

            Vector3 selectedPosition = selectedToBeFocused.position;

            Vector3 desiredPlayerCameraForwardVector = selectedPosition - playerCamera.transform.position;
            Vector3 currentPlayerCameraForwardVector = playerCamera.transform.forward;

            Quaternion camRotation = Quaternion.LookRotation(desiredPlayerCameraForwardVector);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, camRotation, (timeCount * focusDamping));

            timeCount += Time.deltaTime;

            float angle = Vector3.Angle(currentPlayerCameraForwardVector, desiredPlayerCameraForwardVector);
            if (angle <= toleranceAngle)
            {
                ShowPopOut();
            }

            if (angle <= 0)
            {
                focusing = false;
                selectedToBeFocused = null;
                timeCount = 0f;
                if (firstPersonCamera != null)
                {
                    firstPersonCamera.EnableCameraMovement();
                }
            }

        }

        
    }

    private void ShowPopOut()
    {
        if (popOutObject != null && popOutObjectAnimController != null)
        {
            popOutObject.SetActive(true);
            popOutObjectAnimController.ShowPopOut();
        }
        popOutObject = null;
        popOutObjectAnimController = null;

    }

}
