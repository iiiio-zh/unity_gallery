using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
    [SerializeField] GameObject environment;
    [SerializeField] Material switchToMaterial;

    public void SwithEnvironmentMaterial()
    {
        Renderer environmentRenderer = environment.GetComponent<Renderer>();

        Debug.Log("switch env!! " + transform.name + " " + transform.gameObject.activeSelf);


        if (environmentRenderer != null && this.gameObject.activeSelf)
        {
            //Debug.Log("has enev material!");

            StartCoroutine(SwitchMaterialCoroutine(environmentRenderer, switchToMaterial));
        }

    }

    IEnumerator SwitchMaterialCoroutine(Renderer renderer, Material switchToMaterial)
    {
        renderer.material = switchToMaterial;
        yield return null;
        
    }


}
