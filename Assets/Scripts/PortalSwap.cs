using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSwap : MonoBehaviour
{
    public Transform swp;
    public string tag1;
    public string tag2;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < swp.transform.childCount; i++) {
            Transform child = swp.transform.GetChild(i);
            child.transform.position += new Vector3(0, 100, 0);
            Transform cam = child.transform.GetChild(1);
            PortalCamera sc = cam.gameObject.GetComponent<PortalCamera>();
            if (sc.playerTag == tag1)
                sc.playerTag = tag2;
            else if (sc.playerTag == tag2)
                sc.playerTag = tag1;

            Transform col = child.transform.GetChild(0).GetChild(2);
            
            PortalTeleport pt = col.gameObject.GetComponent<PortalTeleport>();
            if (pt is not null) {
                if (pt.playerTag == tag1)
                    pt.playerTag = tag2;
                else if (pt.playerTag == tag2)
                    pt.playerTag = tag1;
            }
            col = child.transform.GetChild(0).GetChild(1);
            pt = col.gameObject.GetComponent<PortalTeleport>();
            if (pt is not null) {
                if (pt.playerTag == tag1)
                    pt.playerTag = tag2;
                else if (pt.playerTag == tag2)
                    pt.playerTag = tag1;
            }
        }
              
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
