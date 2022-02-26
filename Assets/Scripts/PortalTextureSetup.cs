using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{

    public Camera cameraB;
    public Material cameraMatB;
    public Camera cameraW;
    public Material cameraMatW;
    // Start is called before the first frame update
    void Start()
    {
        if (cameraB.targetTexture != null)
            cameraB.targetTexture.Release();

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        cameraMatB.mainTexture = cameraB.targetTexture;

        if (cameraW.targetTexture != null)
            cameraW.targetTexture.Release();

        cameraW.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        cameraMatW.mainTexture = cameraW.targetTexture;
    }

}
