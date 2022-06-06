using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDuplication : MonoBehaviour
{
    public Transform duplicate;
    public Material mat1;
    public Material mat2;
    // Start is called before the first frame update

    void Swap(Transform tf) {
        GameObject go = tf.gameObject;
        try {
            Material mat = go.GetComponent<Renderer>().material;
            if (mat.name.Substring(0, mat1.name.Length) == mat1.name) {
                Debug.Log("SWAPPING");
                go.GetComponent<Renderer>().material = mat2;
            }
            else if (mat.name.Substring(0, mat2.name.Length) == mat2.name) {
                go.GetComponent<Renderer>().material = mat1;
            }
            else {
                Debug.Log($"{mat.name}, {mat1.name}, {mat2.name}");
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
