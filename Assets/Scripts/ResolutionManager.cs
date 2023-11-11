using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private float lastWidth;
    private float lastHeight;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastWidth != Screen.width)
        {
            Screen.SetResolution(Screen.width, (int)(Screen.width * (9f / 16f)), true);
        }
        else if(lastHeight != Screen.height)
        {
            Screen.SetResolution((int)(Screen.height * (16f / 9f)), Screen.height, true);
        }

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }
}
