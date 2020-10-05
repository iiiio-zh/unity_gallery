using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject[] sceneCollection;

    int activeSceneIndex = 0;

    public void Start()
    {
        ChangeActiveScene(activeSceneIndex);
    }

    public void ChangeActiveScene(int index)
    {
        if (sceneCollection.Length > 0 && index < sceneCollection.Length && index >= 0)
        {
            activeSceneIndex = index;

            for (int i = 0; i < sceneCollection.Length; i++)
            {

                if (i == activeSceneIndex)
                {
                    sceneCollection[i].SetActive(true);
                }
                else
                {
                    sceneCollection[i].SetActive(false);
                }
            }

            
        }
           
    }

}
