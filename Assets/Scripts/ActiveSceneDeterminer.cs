using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSceneDeterminer : MonoBehaviour
{
    [SerializeField] private int activeSceneIndex;

    public int ActiveSceneIndex { get => activeSceneIndex; set => activeSceneIndex = value; }


}
