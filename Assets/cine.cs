using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cine : MonoBehaviour
{
    private int frames;
    // Start is called before the first frame update
    public void SkipButton()
    {
        this.gameObject.SetActive(false);
        
    }
}
