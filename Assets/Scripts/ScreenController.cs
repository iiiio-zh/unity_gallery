using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        //Screen.SetResolution(screenWidth, screenHeight, true);
    }
    
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

}
