using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public string playerTag;
    private GameObject player;
    public Transform portal1;
    public Transform portal2;
    
    private Transform camera;

    void Start(){
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (player is null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag);
            if (player is not null)
                camera = player.transform.GetChild(0).GetChild(0);
        }
        else
        {
            Debug.Log(player);
            transform.position = camera.position;
            transform.rotation = camera.rotation;

        //float angle = -Quaternion.Angle(portal1.rotation, portal2.rotation);
            float angle = portal2.transform.eulerAngles.y - portal1.transform.eulerAngles.y;

            transform.RotateAround(portal1.position, Vector3.up, angle);
            Vector3 offset = portal2.position - portal1.position;

            transform.position += offset; 
        }   
    }
}
