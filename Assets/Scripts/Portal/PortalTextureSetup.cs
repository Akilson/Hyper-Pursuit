using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{

    public Camera cam;
    public Material cameraMat;
    // Start is called before the first frame update
    void Start()
    {
        if (cam.targetTexture != null)
            cam.targetTexture.Release();

        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        cameraMat.mainTexture = cam.targetTexture;
    }

}
