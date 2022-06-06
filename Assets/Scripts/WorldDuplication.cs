using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDuplication : MonoBehaviour
{
    public Transform duplicate;
    public Material mat1;
    public Material mat2;
    public string tag1;
    public string tag2;
    // Start is called before the first frame update

    void Swap(Transform tf) {
        GameObject go = tf.gameObject;

        // Try swap material color
        try {
            Material mat = go.GetComponent<Renderer>().material;
            if (mat.name.Substring(0, mat1.name.Length) == mat1.name) {
                go.GetComponent<Renderer>().material = mat2;
            }
            else if (mat.name.Substring(0, mat2.name.Length) == mat2.name) {
                go.GetComponent<Renderer>().material = mat1;
            }
        }
        catch (MissingComponentException) {}

        // Try swap targetted player by PortalTeleport
        try {
            PortalTeleport sc = go.GetComponent<PortalTeleport>();
            if (sc is null)
                Debug.Log("null !");
            else { 
            if (sc.playerTag == tag1)
                sc.playerTag = tag2;
            else if (sc.playerTag == tag2)
                sc.playerTag = tag1;

            }
        }
        catch (MissingComponentException) {}
        
        // Try swap targetted player by PortalCamera
        try {
            PortalCamera sc = go.GetComponent<PortalCamera>();
            if (sc is null)
                Debug.Log("null !");
            else { 
            if (sc.playerTag == tag1)
                sc.playerTag = tag2;
            else if (sc.playerTag == tag2)
                sc.playerTag = tag1;

            }
        }
        catch (MissingComponentException) {}

        for (int i = 0; i < tf.transform.childCount; i++) {
            Transform child = tf.transform.GetChild(i);
            Swap(child);
        }
    }

    void Start()
    {
        Transform dup = Transform.Instantiate(duplicate);
        dup.transform.position += new Vector3(0, 100, 0);
        Swap(dup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
